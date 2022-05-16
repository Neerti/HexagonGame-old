using System;
using System.Collections.Generic;
using Godot;
using Hexagon.Globals;

namespace Hexagon.UI.TechTreeDisplay
{
	public class TechUITree : GraphEdit
	{
		public List<TechUINode> TechUINodes = new List<TechUINode>();
		private const int NodeHorizontalSpacing = 400;
		
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
				TechUINodes.Add(techNode);
			}

			ArrangeTree();

		}

		public void ArrangeTree()
		{
			foreach (var node in TechUINodes)
			{
				var nodePos = new Vector2(NodeHorizontalSpacing * node.NodeToDisplay.FarthestDistanceFromRoot, 0);
				node.Offset = nodePos;

				foreach (var child in node.NodeToDisplay.Children)
				{
					ConnectNode(node.Name, 0, child.TechID.ToString(), 0);
				}
			}

			foreach (var rootNode in Singleton.ScienceTechTree.RootNodes)
			{
				
			}
		}
	}
}

