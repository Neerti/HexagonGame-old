using System.Collections.Generic;
using Godot;
using Hexagon.Items;
using Hexagon.Populations;

namespace Hexagon.Settlements
{
	public class Settlement : Node
	{
		public List<Person> Population;

		public void SimulateConsumption()
		{
			var total_food_need = 0f;
			var total_water_need = 0f;

			foreach (var person in Population)
			{
				total_food_need += person.GetFoodRequirement();
				total_water_need += person.GetWaterRequirement();
			}
		}
		
	}
}

