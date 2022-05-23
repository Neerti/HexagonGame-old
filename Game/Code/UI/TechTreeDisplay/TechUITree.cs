using System.Collections.Generic;
using Godot;
using Hexagon.Globals;
using Hexagon.Research.TechTrees;

namespace Hexagon.UI.TechTreeDisplay
{
	public class TechUITree : GraphEdit
	{
		private Dictionary<TechnologyNode, TechUINode> TechUINodes = new Dictionary<TechnologyNode, TechUINode>();
		private const int NodeHorizontalSpacing = 400;
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
				techNode.MouseFilter = MouseFilterEnum.Stop;
				
				TechUINodes.Add(Singleton.ScienceTechTree.Nodes[id], techNode);
			}

			ArrangeTree();

		}

		private void ArrangeTree()
		{
			foreach (var node in TechUINodes.Values)
			{
				// The horizontal position is based on the node's calculated depth.
				var nodePos = new Vector2(NodeHorizontalSpacing * node.NodeToDisplay.FarthestDistanceFromRoot, 0);
				node.Offset = nodePos;

				// Make lines between the nodes.
				foreach (var child in node.NodeToDisplay.Children)
				{
					ConnectNode(node.Name, 0, child.TechID.ToString(), 0);
				}
			}

			var currentRow = 0;
			foreach (var rootNode in Singleton.ScienceTechTree.RootNodes)
			{
				var sortedNodes = DepthFirstSort(rootNode);

				for (int i = 0; i < sortedNodes.Count; i++)
				{
					GD.Print(i + " | " + sortedNodes[i]);
				}

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
				//GD.Print("Current node is now " + currentNode);
				
				visited.Add(currentNode);
				//GD.Print("Added " + currentNode);
				foreach (var childNode in currentNode.Children)
				{
					if (visited.Contains(childNode))
					{
						//GD.Print("Visited list already contains " + childNode);
						continue;
					}
					stack.Push(childNode);
					//GD.Print("Adding " + childNode + " to stack.");
				}

			} while (stack.Count > 0);

			return visited;
		}
		
		

	
	}
}

