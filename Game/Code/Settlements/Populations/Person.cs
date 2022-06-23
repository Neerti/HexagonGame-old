using System;
using System.Collections.Generic;
using Godot;
using Hexagon.Globals;

namespace Hexagon.Populations
{
	public enum PersonLifecycle
	{
		Baby,
		Child,
		Adult,
		Elder
	}
	
	public class Person : Node
	{
		/// <summary>
		/// The in-game birthday for this particular person. Used to calculate the current age.
		/// </summary>
		public DateTime Birthday { get; private set; }

		public PersonLifecycle CurrentLifecycle = PersonLifecycle.Adult;
		
		public List<NeedKinds> Needs = new List<NeedKinds>();

		// TODO replace with generic Need subclasses.
		public float Starvation = 0f;
		public float Dehydration = 0f;
		public float NeedReplenishmentPerDay = 0.5f;

		public override void _Ready()
		{
			// TODO: Have this populated by species or something.
			Needs.Add(NeedKinds.Food);
			Needs.Add(NeedKinds.Water);
		}

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
			GD.Print(Name + " ate " + amount + " and has a starvation value of " + Starvation);
		}

		public float ScaleNeedRequirement()
		{
			switch (CurrentLifecycle)
			{
				case PersonLifecycle.Baby:
					return 0.25f;
				case PersonLifecycle.Child:
					return 0.50f;
				default:
					return 1.0f;
			}
		}

		public bool HasNeed(NeedKinds id)
		{
			return Needs.Contains(id);
		}

		public NeedTicket GetNeedTicket()
		{
			var request = new Dictionary<NeedKinds, float>();
			foreach (var need in Needs)
			{
				request[need] = Singleton.AllNeeds[need].GetNeededAmount(this);
			}

			return new NeedTicket(this, request);
		}

		public void FulfillNeed(NeedKinds kind, float amount)
		{
			// TODO: Implement.
			GD.Print("Was given " + amount + " to fulfill " + kind);
		}
	}
}