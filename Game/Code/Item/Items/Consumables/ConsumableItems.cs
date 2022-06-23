using Godot;

namespace Hexagon.Items
{
	/// <summary>
	/// Consumable in this context refers to any item which can be eaten or drank.
	/// </summary>
	public class ConsumableItem : Item
	{
		/// <summary>
		/// Determines how filling a particular food is.
		/// By default, every person needs 1.0f nourishment's worth of food every day to avoid malnutrition penalties
		/// or starvation. Certain people may need (or expect) more or less food per day.
		/// </summary>
		public float Nourishment { get; protected set; } = 0.25f;

		/// <summary>
		/// Determines how tasty something is.
		/// Positive numbers are enjoyable to eat, while negatives are the opposite.
		/// By default, people in the game seek out the best tasting food first and avoid bad tasting food if possible.
		/// It's possible to override that with policies.
		/// Having to eat bad tasting food will lower morale.
		/// </summary>
		public int Joy { get; protected set; }

		/// <summary>
		/// Some consumables may be healthy, others the opposite. Positive numbers improve the health of anyone who
		/// eats it, while negatives provide a detriment.
		/// </summary>
		public int Healthy { get; protected set; }
		
		public ConsumableItem()
		{
			DisplayColor = Colors.Green;
			GameplayDescription = "Used as food.";
			Category = ItemCategory.Consumable;
		}
	}

	public class Berry : ConsumableItem
	{
		public Berry()
		{
			ItemID = ItemIDs.Berry;
			DisplayName = "Berry";
			DisplayNamePlural = "Berries";
			FluffDescription = "A small, tasty fruit. Not very filling, but easy to forage.";
			
			Joy = 1;
			Nourishment = 0.20f;
		}
	}
	
	public class Apple : ConsumableItem
	{
		public Apple()
		{
			ItemID = ItemIDs.Apple;
			DisplayName = "Apple";
			DisplayNamePlural = "Apples";
			FluffDescription = "A fruit that grows on apple trees.";
			
			Joy = 1;
			Nourishment = 0.25f;
			Healthy = 1;
		}
	}
	
	public class SpoiledFood : ConsumableItem
	{
		public SpoiledFood()
		{
			ItemID = ItemIDs.SpoiledFood;
			DisplayName = "Spoiled Food";
			FluffDescription = "While technically still edible, it would be a bad idea.";
			GameplayDescription = "Can be used as food, if desperate.";
			
			Joy = -2;
			Nourishment = 0.1f;
			Healthy = -2;
		}
	}
}