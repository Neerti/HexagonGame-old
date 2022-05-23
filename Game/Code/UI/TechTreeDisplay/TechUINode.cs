using Godot;
using Hexagon.Research.TechTrees;
using Hexagon.Globals;

namespace Hexagon.UI.TechTreeDisplay
{
	public class TechUINode : GraphNode
	{
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
			// someday...
			
			// ShortDescription label.
			var shortDescLabel = GetNode<Label>("ShortDescriptionLabel");
			shortDescLabel.Text = NodeToDisplay.ShortDescription;
			
			// Description.
			// TODO: Port wordwrap function from prior Autonomy project.
			//HintTooltip = NodeToDisplay.Description;
			
			// Connections.
			SetSlotEnabledLeft(0, NodeToDisplay.Parents.Count > 0);
			SetSlotEnabledRight(0, NodeToDisplay.Children.Count > 0);
			
			/*var maxLength = Mathf.Max(NodeToDisplay.Parents.Count, NodeToDisplay.Children.Count);
			var packedRow =
				(PackedScene) ResourceLoader.Load("res://Code/UI/TechTreeDisplay/TechNodeConnectionRow.tscn");
			for (var i = 0; i < maxLength; i++)
			{
				var row = (HBoxContainer)packedRow.Instance();
				var parentButton = row.GetNode<Button>("ParentButton");
				var childButton = row.GetNode<Button>("ChildButton");
				
				if (i <= NodeToDisplay.Parents.Count - 1)
				{
					parentButton.Text = NodeToDisplay.Parents[i].DisplayName;
				}
				else
				{
					parentButton.Hide();
				}
				
				if (i <= NodeToDisplay.Children.Count - 1)
				{
					childButton.Text = NodeToDisplay.Children[i].DisplayName;
				}
				else
				{
					childButton.Hide();
				}
					
				AddChild(row);
			}*/
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
				}
			}
			
		}
		
	}	
}

