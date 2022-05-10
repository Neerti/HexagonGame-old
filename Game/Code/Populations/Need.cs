namespace Hexagon.Populations
{
	/// <summary>
	/// Holds information about a particular thing people need in order to continue existing.
	/// </summary>
	public abstract class Need
	{
		/// <summary>
		/// The name of the Need, shown in UIs to the player.
		/// </summary>
		protected string DisplayName;
		
		/// <summary>
		/// A short description of the Need, shown in UIs to the player.
		/// </summary>
		protected string Description;
		
		/// <summary>
		/// The default amount of resources needed per day to satisfy the Need. Other modifiers may increase or
		/// decrease the actual amount required.
		/// </summary>
		protected float BaseRequirementPerDay = 1.0f;
		
		protected float MaxDeficit;
		
		/// <summary>
		/// Determines how much extra resources can be consumed to 'pay back' a need deficit.
		/// The value is a multiplier of the base requirement, so that it can scale with it if needed.
		/// </summary>
		protected float DeficitReplenishmentMultiplierPerDay = 0.5f; // 50% of base amount.

		public override string ToString()
		{
			return DisplayName;
		}
	}
	

	public class FoodNeed : Need
	{
		public FoodNeed()
		{
			DisplayName = "Hunger";
			MaxDeficit = 21.0f;
		}
	}
	
	public class WaterNeed : Need
	{
		public WaterNeed()
		{
			DisplayName = "Thirst";
			MaxDeficit = 3.0f;
		}
	}
}