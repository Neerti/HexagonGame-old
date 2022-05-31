using Godot;
using Hexagon.Research.TechTrees;

namespace Hexagon.UI.TechTreeDisplay
{
	public class TechnologyInfoRows : VBoxContainer
	{
		public void DescribeTechnology(TechnologyNode tech)
		{
			var texture = GetNode<TextureRect>("CenterContainer/TechnologyTextureRect");
			texture.Texture = ResourceLoader.Load<Texture>(tech.TexturePath);

			GetNode<Label>("TechnologyNameLabel").Text = tech.DisplayName;

			GetNode<Label>("ShortDescriptionLabel").Text = tech.ShortDescription;
			
			GetNode<Label>("DescriptionLabel").Text = tech.Description;
			
			GetNode<Label>("GameplayEffectsLabel").Text = tech.GameplayEffects;
		}
	}
}


