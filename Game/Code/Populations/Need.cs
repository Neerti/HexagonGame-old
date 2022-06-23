namespace Hexagon.Populations
{
	/// <summary>
	/// Holds information about a particular thing people need in order to continue existing.
	/// </summary>
	public abstract class Need
	{
		public NeedKinds NeedKind { get; protected set; }
		
		/// <summary>
		/// The name of the Need, shown in UIs to the player.
		/// </summary>
		public string DisplayName { get; protected set; }
		
		/// <summary>
		/// A short description of the Need, shown in UIs to the player.
		/// </summary>
		protected string Description;
		
		/// <summary>
		/// The default amount of resources needed per day to satisfy the Need. Other modifiers may increase or
		/// decrease the actual amount required.
		/// </summary>
		protected float BaseRequirementPerDay = 1.0f;
		
		/// <summary>
		/// How much of a deficit can be accumulated. The actual number is based on the daily requirement multiplied
		/// by this number.
		/// </summary>
		protected float MaxDeficitMultiplier = 1.0f;
		
		/// <summary>
		/// Determines how much extra resources can be consumed to 'pay back' a need deficit.
		/// The value is a multiplier of the base requirement, so that it can scale with it if needed.
		/// </summary>
		protected float DeficitReplenishmentMultiplierPerDay = 0.5f; // 50% of base amount.

		public override string ToString()
		{
			return DisplayName;
		}

		/// <summary>
		/// Returns how much of something is required to satisfy the Need for a day.
		/// </summary>
		/// <returns>Amount required per day.</returns>
		public float GetNeededAmount(Person who)
		{
			return BaseRequirementPerDay * who.ScaleNeedRequirement();
		}
	}
	

	public class FoodNeed : Need
	{
		public FoodNeed()
		{
			NeedKind = NeedKinds.Food;
			DisplayName = "Hunger";
			MaxDeficitMultiplier = 21.0f;
		}
	}
	
	public class WaterNeed : Need
	{
		public WaterNeed()
		{
			NeedKind = NeedKinds.Water;
			DisplayName = "Thirst";
			MaxDeficitMultiplier = 3.0f;
		}
	}

	public enum NeedKinds
	{
		Food,
		Water
	}
}