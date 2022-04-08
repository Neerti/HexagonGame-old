using Godot;

namespace Hexagon.Items
{
	public enum ItemIDs
	{
		Base = 0,
		Stick = 1,
		Stone = 2,
		FreshWater = 3,
		Berry = 4,
		Inspiration = 5
	}
	
	/// <summary>
	/// Items are not individual instances, but are instead an aggregate of a specific idea 
	/// of a physical or conceptual object, such as sticks or rocks.
	/// </summary>
	public abstract class Item
	{
		/// <summary>
		/// Internal identification enum for items. 
		/// </summary>
		public ItemIDs ItemID { get; protected set; } = ItemIDs.Base;

		/// <summary>
		/// The name of the <see cref="Item"/>, displayed in UIs.
		/// </summary>
		public string DisplayName { get; protected set; }

		/// <summary>
		/// Describes what the <see cref="Item"/> might be used for in gameplay.
		/// </summary>
		public string GameplayDescription { get; protected set; }

		/// <summary>
		/// A short description about the <see cref="Item"/>, to add flavor.
		/// </summary>
		public string FluffDescription { get; protected set; }

		/// <summary>
		/// Color that should be used to display this <see cref="Item"/>'s name.
		/// </summary>
		public Color DisplayColor { get; protected set; } = Colors.White;

		// TODO sprite here.

		/// <summary>
		/// Used to categorize different kinds of <see cref="Item"/>s together.
		/// </summary>
		public ItemCategory Category { get; protected set; } = ItemCategory.General;

		public enum ItemCategory
		{
			General = 1,
			Consumable = 2,
			Scientific = 3,
			Cultural = 4
		}

		public override string ToString()
		{
			return DisplayName;
		}
	}
}

