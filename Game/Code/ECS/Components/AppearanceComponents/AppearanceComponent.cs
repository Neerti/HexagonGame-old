using Godot;

namespace Hexagon.ECS.Components.AppearanceComponents
{
	/// <summary>
	/// Appearance Components hold data that is relevant to visually representing the entity in the world.
	/// </summary>
	public class AppearanceComponent : Component
	{
		public string MapTexturePath;
		public Color MapTextureColor = Colors.White;
	}
}