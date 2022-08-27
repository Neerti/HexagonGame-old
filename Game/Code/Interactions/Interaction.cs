using JetBrains.Annotations;
using Godot;

namespace Hexagon.Interactions
{
	/// <summary>
	/// Holds information for the UI buttons shown on the bottom of the screen.
	/// Subclasses contain more functionality, such as containing more Interactions, toggling a UI window, or selecting
	/// a building.
	/// </summary>
	public abstract class Interaction
	{
		/// <summary>
		/// Name of the button, shown in the UI to the player, both underneath the texture, and
		/// in tooltips.
		/// </summary>
		[UsedImplicitly]
		public string DisplayName { get; protected set; }
		
		/// <summary>
		/// A short description about the button, shown in the UI to the player in tooltips.
		/// </summary>
		[UsedImplicitly]
		public string DisplayDescription { get; protected set; }

		/// <summary>
		/// A color that the button shifts to when moused over.
		/// </summary>
		[UsedImplicitly]
		public Color DisplayColor { get; protected set; } = Colors.White;
		
		/// <summary>
		/// Path to the texture shown in the button.
		/// </summary>
		[UsedImplicitly]
		public string TexturePath { get; protected set; }
		
		/// <summary>
		/// Determines if a particular button (or group of buttons) should be visible to the player.
		/// </summary>
		/// <returns>True if the button should be seen, false otherwise.</returns>
		public virtual bool ShouldBeVisible()
		{
			return true;
		}                                                  
	}
}           