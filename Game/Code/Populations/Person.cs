using System;
using System.Collections.Generic;

namespace Hexagon.Populations
{
	public enum PersonLifecycle
	{
		Baby,
		Child,
		Adult,
		Elder
	}
	
	public class Person
	{
		/// <summary>
		/// The in-game birthday for this particular person. Used to calculate the current age.
		/// </summary>
		public DateTime Birthday { get; private set; }

		public PersonLifecycle CurrentLifecycle = PersonLifecycle.Adult;

		// TODO replace with generic Need subclasses.
		public float Starvation = 0f;
		public float Dehydration = 0f;
		public float NeedReplenishmentPerDay = 0.5f;


		public void NewlyBorn(DateTime currentDate)
		{
			Birthday = currentDate;
			CurrentLifecycle = PersonLifecycle.Baby;
		}
		
		public TimeSpan GetAge(DateTime currentDate)
		{
			var result = currentDate - Birthday;
			return currentDate - Birthday;
		}

		/// <summary>
		/// Determines how much food per day a specific person needs to not
		/// </summary>
		/// <returns></returns>
		public float GetFoodNeed()
		{
			return 1.0f;
		}
		
		public float GetFoodWant()
		{
			if (Starvation > 0)
			{
				return GetFoodNeed() * NeedReplenishmentPerDay;
			}
			return GetFoodNeed();
		}
		

		public float GetWaterNeed()
		{
			return 1.0f;
		}
		
		public float GetWaterWant()
		{
			if (Dehydration > 0)
			{
				return GetWaterNeed() * NeedReplenishmentPerDay;
			}
			return GetWaterNeed();
		}

		public void Eat(float amount)
		{
			var needed_food = GetFoodNeed();
			if (amount > needed_food)
			{
				if (Starvation > 0)
				{
					var excess = amount - needed_food;
					Starvation = Math.Max(Starvation - excess, 0);
				}
			}
			else if (amount < needed_food)
			{
				var deficit = needed_food - amount;
				Starvation += deficit;
			}
		}
	}
}