using Godot;
using Hexagon.Globals;
using Hexagon.Research.TechTrees;

namespace Hexagon.UI.TechTreeDisplay
{
	public class NodeDisplay : PanelContainer
	{
		public TechnologyNode TechToDisplay;

		public void DisplayTechnology(TechIDs techID)
		{
			var node = Singleton.ScienceTechTree.GetNode(techID);
			DisplayTechnology(node);
		}
		
		public void DisplayTechnology(TechnologyNode techNode)
		{
			TechToDisplay = techNode;
			
			// Name (of the Godot node).
			Name = TechToDisplay.TechID.ToString();
			
			// Title.
			GetNode<Label>("Content/TitleLabel").Text = TechToDisplay.DisplayName;

			// Texture.
			GetNode<TextureRect>("Content/CenterContainer/TextureRect").Texture =
				ResourceLoader.Load<Texture>(TechToDisplay.TexturePath);
			
			// ShortDescription label.
			GetNode<Label>("Content/ShortDescriptionLabel").Text = TechToDisplay.ShortDescription;
		}
	} 
}


