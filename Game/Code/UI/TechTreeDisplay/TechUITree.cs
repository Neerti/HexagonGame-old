using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
using Hexagon.Globals;
using Hexagon.Research.TechTrees;

namespace Hexagon.UI.TechTreeDisplay
{
	public class TechUITree : GraphEdit
	{
		private Dictionary<TechnologyNode, TechUINode> TechUINodes = new Dictionary<TechnologyNode, TechUINode>();
		private const int NodeHorizontalSpacing = 300;
		private const int NodeVerticalSpacing = 200;
		
		public override void _Ready()
		{
			// Hide the buttons that shows on the top left of the GraphEdit object.
			// Unfortunately there isn't a normal and sane option to disable the buttons so instead they need to be
			// hidden at runtime. A future engine update will probably break this but perhaps by then a normal way 
			// will be available.
			var graphEditBar = GetNode<HBoxContainer>("@@2/@@4");
			graphEditBar.Visible = false;

			// Now spawn all the nodes.
			var packedNode =
				(PackedScene) ResourceLoader.Load("res://Code/UI/TechTreeDisplay/TechUINode.tscn");
			foreach (var id in Singleton.ScienceTechTree.Nodes.Keys)
			{
				var techNode = (TechUINode)packedNode.Instance();
				techNode.DisplayNode(id);
				AddChild(techNode);
				// GraphEdit seems to really insist on this being Pass.
				techNode.MouseFilter = MouseFilterEnum.Stop;
				
				TechUINodes.Add(Singleton.ScienceTechTree.Nodes[id], techNode);
			}

			ArrangeTree();

			ScrollOffset = new Vector2(-20, -20);

		}

		private void ArrangeTree()
		{
			foreach (var node in TechUINodes.Values)
			{
				// Make lines between the nodes.
				foreach (var child in node.NodeToDisplay.Children)
				{
					ConnectNode(node.Name, 0, child.TechID.ToString(), 0);
				}
			}

			var currentRow = 0;

			foreach (var rootNode in Singleton.ScienceTechTree.RootNodes)
			{
				//var sortedNodes = DepthFirstSort(rootNode);
				var sortedNodes = TopologicalSort(rootNode, new Stack<TechnologyNode>(),
					new Dictionary<TechnologyNode, bool>()).ToList();
				var distances = new Dictionary<TechnologyNode, int>();
				foreach (var node in sortedNodes)
				{
					distances[node] = int.MinValue;
					GD.Print(node);
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
					GD.Print(thing);
					
					var displayNode = TechUINodes[thing.Key];
					displayNode.Offset = new Vector2(thing.Value * NodeHorizontalSpacing, displayNode.Offset.y);
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

					var displayNode = TechUINodes[node];
					displayNode.Offset = new Vector2(displayNode.Offset.x, currentRow * NodeVerticalSpacing);

					j++;
				}

				var result = TopologicalSort(rootNode, new Stack<TechnologyNode>(),
					new Dictionary<TechnologyNode, bool>());
				GD.Print("TopologicalSort results;");
				foreach (var thing in result)
				{
					GD.Print(thing);
				}
				
				currentRow++;



			}
			
			
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

