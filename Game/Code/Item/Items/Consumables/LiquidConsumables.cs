namespace Hexagon.Items
{
	/// <summary>
	/// Abstract class for consumables in liquid form, used to fulfill hydration need.
	/// </summary>
	public abstract class LiquidConsumables : ConsumableItem
	{
		protected LiquidConsumables()
		{
			Nourishment = 1.0f;
			GameplayDescription = "Consumed to avoid dying of thirst.";
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