using Godot;
using Godot.Collections;
using Hexagon.Populations;

namespace Hexagon.Items
{
	public enum ItemIDs
	{
		Base,
		Stick,
		Stone,
		CleanWater,
		Berry,
		Apple,
		Inspiration,
		Theory,
		Paper,
		SpoiledFood,
		ContaminatedWater,
		Tea
	}
	
	public enum ItemCategory
	{
		General = 1,
		Consumable = 2,
		Scientific = 3,
		Cultural = 4
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
		/// The name of the <see cref="Item"/>, displayed in UIs, generally in singular form.
		/// </summary>
		protected string DisplayName { get; set; }
		
		/// <summary>
		/// The name of multiple <see cref="Item"/>s, displayed in UIs. If empty, <see cref="DisplayName"/> is
		/// used instead.
		/// </summary>
		protected string DisplayNamePlural { get; set; }

		/// <summary>
		/// Describes what the <see cref="Item"/> might be used for in gameplay.
		/// </summary>
		protected string GameplayDescription { get; set; }

		/// <summary>
		/// A short description about the <see cref="Item"/>, to add flavor.
		/// </summary>
		protected string FluffDescription { get; set; }

		/// <summary>
		/// Color that should be used to display this <see cref="Item"/>'s name.
		/// </summary>
		protected Color DisplayColor { get; set; } = Colors.White;

		// TODO sprite here.

		/// <summary>
		/// Used to categorize different kinds of <see cref="Item"/>s together.
		/// </summary>
		protected ItemCategory Category { get; set; } = ItemCategory.General;

		protected float Volume { get; set; } = 1.0f;

		/// <summary>
		/// Some items can fulfill specific <see cref="Need"/>s that a <see cref="Person"/> may have,
		/// such as water providing hydration.
		/// </summary>
		public Dictionary<NeedKinds, float> NeedFulfill { get; protected set; } = new Dictionary<NeedKinds, float>();

		public override string ToString()
		{
			return DisplayName;
		}
	}
}

