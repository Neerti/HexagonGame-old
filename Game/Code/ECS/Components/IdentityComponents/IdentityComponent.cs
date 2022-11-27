using Godot;

namespace Hexagon.ECS.Components.IdentityComponents
{
	/// <summary>
	/// Identity components holds data for players to distinguish entities between each other, (mostly) textually.
	/// Tooltips and UI elements will look for this component in order to display things like
	/// names and flavor text.
	/// </summary>
	public class IdentityComponent : Component
	{
		/// <summary>
		/// The 'pretty name' for the entity.
		/// </summary>
		public string DisplayName;
		
		/// <summary>
		/// A short description about the entity.
		/// </summary>
		public string Description;
		
		/// <summary>
		/// File path to the image file that should represent the entity in UI elements.
		/// </summary>
		public string UITexturePath;
		
		/// <summary>
		/// Colors the image obtained from <see cref="UITexturePath"/> in UI elements.
		/// For best results, use a monochromatic image.
		/// Will not have any effect if set to <see cref="Colors.White"/>.
		/// </summary>
		public Color UITextureColor = Colors.White;
	}
}


