using Hexagon.Populations;

namespace Hexagon.Items
{
	/// <summary>
	/// Abstract class for consumables in liquid form, used to fulfill hydration need.
	/// </summary>
	public class LiquidConsumables : ConsumableItem
	{
		public LiquidConsumables()
		{
			GameplayDescription = "Consumed to avoid dying of thirst.";
			NeedFulfill.Add(NeedKinds.Water, 1.0f);
		}
	}
	
	public class CleanWater : LiquidConsumables
	{
		public CleanWater()
		{
			ItemID = ItemIDs.CleanWater;
			DisplayName = "Clean Water";
			FluffDescription = "Water that is clear and safe to drink.";
			
		}
	}
	
	public class ContaminatedWater : LiquidConsumables
	{
		public ContaminatedWater()
		{
			ItemID = ItemIDs.ContaminatedWater;
			DisplayName = "Contaminated Water";
			FluffDescription = "Dirty, polluted water that is likely to make someone sick, or worse.";

			Joy = -2;
			Healthy = -2;

		}
	}
	
	public class Tea : LiquidConsumables
	{
		public Tea()
		{
			ItemID = ItemIDs.Tea;
			DisplayName = "Tea";
			FluffDescription = "Warm and delicious tea, prepared using tea leaves and hot water.";

			Joy = 1;
			Healthy = 1;

		}
	}
}