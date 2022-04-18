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


		public TimeSpan GetAge(DateTime currentDate)
		{
			var result = currentDate - Birthday;
			return currentDate - Birthday;
		}

		public float GetFoodRequirement()
		{
			return 1.0f;
		}

		public float GetWaterRequirement()
		{
			return 1.0f;
		}
	}
}