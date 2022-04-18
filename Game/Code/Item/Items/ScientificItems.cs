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
			FluffDescription = "An idea, crystallized in the mind.";
			ItemID = ItemIDs.Inspiration;
			Volume = 0f; // Ideas have no physical form.
		}
	}
}
