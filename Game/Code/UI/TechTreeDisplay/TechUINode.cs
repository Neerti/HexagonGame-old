using Godot;
using Hexagon.Research.TechTrees;
using Hexagon.Globals;

namespace Hexagon.UI.TechTreeDisplay
{
	public class TechUINode : GraphNode
	{
		[Signal]
		public delegate void UINodeClicked(TechnologyNode tech);
		
		public TechnologyNode NodeToDisplay;


		public void DisplayNode(TechIDs techID)
		{
			var node = Singleton.ScienceTechTree.GetNode(techID);
			DisplayNode(node);
		}
		
		public void DisplayNode(TechnologyNode techNode)
		{
			NodeToDisplay = techNode;
			
			// Name (of the Godot node).
			Name = NodeToDisplay.TechID.ToString();
			
			// Title.
			Title = NodeToDisplay.DisplayName;
			
			// Texture.
			
			if (NodeToDisplay.TexturePath != null)
			{
				var texture = GetNode<TextureRect>("CenterContainer/TechNodeTexture");
				texture.Texture = ResourceLoader.Load<Texture>(NodeToDisplay.TexturePath);
			}
			
			
			// ShortDescription label.
			var shortDescLabel = GetNode<Label>("ShortDescriptionLabel");
			shortDescLabel.Text = NodeToDisplay.ShortDescription;
			
			// Description.
			// TODO: Port wordwrap function from prior Autonomy project.
			//HintTooltip = NodeToDisplay.Description;
			
			// Connections.
			SetSlotEnabledLeft(0, NodeToDisplay.Parents.Count > 0);
			SetSlotEnabledRight(0, NodeToDisplay.Children.Count > 0);
			
		}

		
		public override void _Ready()
		{
			// Has a tendency to get reverted in the editor.
			MouseFilter = MouseFilterEnum.Stop;
			
		}

		public override void _GuiInput(InputEvent @event)
		{
			if (@event is InputEventMouseButton click)
			{
				if (click.Pressed)
				{
					// TODO: Have this show up in a side panel or something.
					GD.Print(NodeToDisplay.Description);
					Highlight();
					EmitSignal(nameof(UINodeClicked), NodeToDisplay);
				}
			}
			
		}

		public void Highlight()
		{
			Selected = true;
			
			
			
			/*
			SetSlotColorLeft(0, Colors.Yellow);
			SetSlotColorRight(0, Colors.Yellow);
			
			var treeUI = GetParent<TechUITree>();
			
			foreach (var parent in NodeToDisplay.Parents)
			{
				var parentGraphNode = treeUI.TechUINodes[parent];
				parentGraphNode.Overlay = OverlayEnum.Breakpoint;
				parentGraphNode.SetSlotColorRight(0, Colors.Yellow);
			}
			
			foreach (var child in NodeToDisplay.Children)
			{
				var childGraphNode = treeUI.TechUINodes[child];
				childGraphNode.Overlay = OverlayEnum.Breakpoint;
				childGraphNode.SetSlotColorLeft(0, Colors.Yellow);
			}
			*/
		}
		
	}	
}

