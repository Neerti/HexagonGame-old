using Godot;
using System.Collections.Generic;
using System.Linq;
using Hexagon.Globals;
using Hexagon.Research.TechTrees;

namespace Hexagon.UI.TechTreeDisplay
{
	public class GraphDisplay : Panel
	{
		private const int NodeHorizontalSpacing = 400;
		private const int NodeVerticalSpacing = 300;
		private Godot.Collections.Dictionary<TechIDs, NodeDisplay> _nodeDisplays = new Godot.Collections.Dictionary<TechIDs, NodeDisplay>();

		public override void _Ready()
		{
			// Step one, make one of each tech.
			MakeNodes();

			PositionNodes();

			ConnectNodes();

			MakeScrollable();
		}

		private void MakeNodes()
		{
			var packedNode = (PackedScene) ResourceLoader.Load("res://Code/UI/NewTechTreeDisplay/NodeDisplay.tscn");
			foreach (var id in Singleton.ScienceTechTree.Nodes.Keys)
			{
				var newNode = packedNode.Instance<NodeDisplay>();
				newNode.DisplayTechnology(id);
				
				AddChild(newNode);
				
				_nodeDisplays.Add(id, newNode);
			}
		}

		private void PositionNodes()
		{
			var currentRow = 0;
			
			foreach (var rootNode in Singleton.ScienceTechTree.RootNodes)
			{
				//var sortedNodes = DepthFirstSort(rootNode);
				var sortedNodes = TopologicalSort(
					rootNode, 
					new Stack<TechnologyNode>(),
					new Dictionary<TechnologyNode, bool>()).ToList();
				var distances = new Dictionary<TechnologyNode, int>();
				foreach (var node in sortedNodes)
				{
					distances[node] = int.MinValue;
				}

				distances[rootNode] = 0;
				
				foreach (var vertex in sortedNodes)
				{
					foreach (var edge in vertex.Children)
					{
						if (distances[edge] < distances[vertex] + 1)
						{
							distances[edge] = distances[vertex] + 1;
						}
					}
				}

				foreach (var thing in distances)
				{
					var displayNode = _nodeDisplays[thing.Key.TechID];
					//displayNode.RectPosition = new Vector2(thing.Value * NodeHorizontalSpacing, displayNode.RectPosition.y);
					displayNode.RectPosition = new Vector2(displayNode.RectPosition.x, thing.Value * NodeVerticalSpacing);
				}
				
			}
			
			foreach (var rootNode in Singleton.ScienceTechTree.RootNodes)
			{
				var sortedNodes = DepthFirstSort(rootNode);

				var j = 0;
				foreach (var node in sortedNodes)
				{
					if (j > 0)
					{
						if (!node.Parents.Contains(sortedNodes[j - 1]))
						{
							currentRow++;
						}
					}

					var displayNode = _nodeDisplays[node.TechID];
					//displayNode.RectPosition = new Vector2(displayNode.RectPosition.x, currentRow * NodeVerticalSpacing);
					displayNode.RectPosition = new Vector2(currentRow * NodeHorizontalSpacing, displayNode.RectPosition.y);

					j++;
				}
				currentRow++;
			}
		}

		private void ConnectNodes()
		{
			foreach (var techDisplay in _nodeDisplays.Values)
			{
				foreach (var child in techDisplay.TechToDisplay.Children)
				{
					var childDisplay = _nodeDisplays[child.TechID];
					var line = new Line2D();
					
					line.DefaultColor = Colors.White;
					line.Antialiased = true;
					line.Width = 5;
					
					var one = techDisplay.GetNode<CenterContainer>("Content/CenterContainer");
					var two = childDisplay.GetNode<CenterContainer>("Content/CenterContainer");
					var onePos = new Vector2(
						techDisplay.RectPosition.x + one.RectSize.x / 2 + line.Width,
						techDisplay.RectPosition.y + one.RectSize.y / 2 + line.Width
						);
					
					var twoPos = new Vector2(
						childDisplay.RectPosition.x + two.RectSize.x / 2 + line.Width,
						childDisplay.RectPosition.y + two.RectSize.y / 2 + line.Width
					);
					
					line.AddPoint(onePos);
					line.AddPoint(twoPos);

					//AddChild(line);
					AddChildBelowNode(GetNode<Control>("Lines"), line);
				}
				
				
			}
		}

		private void MakeScrollable()
		{
			var new_x = 0.0f;
			var new_y = 0.0f;
			foreach (var node in _nodeDisplays.Values)
			{
				if (node.RectPosition.x + node.RectSize.x > new_x)
				{
					new_x = node.RectPosition.x + node.RectSize.x;
				}
				if (node.RectPosition.y + node.RectSize.y > new_y)
				{
					new_y = node.RectPosition.y + node.RectSize.y;
				}
			}

			RectMinSize = new Vector2(new_x, new_y);
		}
		
		
		private List<TechnologyNode> DepthFirstSort(TechnologyNode rootNode)
		{
			var visited = new List<TechnologyNode>();
			var stack = new Stack<TechnologyNode>();

			stack.Push(rootNode);
			do
			{
				var currentNode = stack.Pop();
				
				visited.Add(currentNode);
				foreach (var childNode in currentNode.Children)
				{
					if (visited.Contains(childNode))
					{
						continue;
					}
					stack.Push(childNode);
				}

			} while (stack.Count > 0);

			return visited;
		}

		private Stack<TechnologyNode> TopologicalSort(TechnologyNode currentNode, Stack<TechnologyNode> stack, Dictionary<TechnologyNode, bool> visited)
		{
			visited[currentNode] = true;
			foreach (var child in currentNode.Children)
			{
				if (!visited.ContainsKey(child))
				{
					TopologicalSort(child, stack, visited);
				}
			}
			stack.Push(currentNode);
			return stack;
		}
	}
}

