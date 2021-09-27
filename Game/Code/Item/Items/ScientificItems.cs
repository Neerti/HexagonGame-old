using Godot;

namespace Hexagon.Items
{
	public class ScientificItem : Item
	{
		public ScientificItem()
		{
			DisplayColor = Colors.Cyan;
			Category = ItemCategory.Scientific;
		}
	}

	public class Inspiration : ScientificItem
	{
		public Inspiration()
		{
			DisplayName = "Inspiration";
			GameplayDescription = "Used in researching new technologies.";
			FluffDescription = "An idea, crystalized in the mind.";
			ItemID = ItemIDs.Inspiration;
		}
	}
}
