using System.Collections.Generic;
using Godot;
using Godot.Collections;
using Hexagon.Globals;
using Hexagon.Items;
using Hexagon.Populations;

namespace Hexagon.Settlements
{
	/// <summary>
	/// Settlements tie together the people, buildings, and resources of a particular geographic location on the map.
	/// They exist to simplify the simulation and avoid players having to do excessive micromanaging, by confining
	/// resource and population logic into discrete geographical points as opposed to a continuous model based
	/// on proximity.
	/// </summary>
	public class Settlement : Node
	{
		public void ProcessSettlement()
		{
			GD.Print(Name + " was processed.");
			// Buildings are always processed first, since otherwise the people might not have their needs met on time.
			ProcessBuildings();
			ProcessPopulation();
		}

		private void ProcessBuildings()
		{
			foreach (var node in GetNode<Node>("Buildings").GetChildren())
			{
				var building = (Building) node;
				building.Process();
			}
		}
		private void ProcessPopulation()
		{
			NeedManager.ProcessNeeds(this);
		}
		

		public List<Person> GetPopulation()
		{
			var result = new List<Person>();
			var populationNode = GetNode<Node>("Population");
			var children = populationNode.GetChildren();
			for (var i = 0; i < children.Count; i++)
			{
				result.Add((Person)children[i]);
			}

			return result;
		}

		public Godot.Collections.Dictionary<ItemIDs, int> GetStorage()
		{
			return new Godot.Collections.Dictionary<ItemIDs, int>()
			{
				[ItemIDs.Apple] = 20,
				[ItemIDs.CleanWater] = 30
			};
		}

		public List<Storage> GetAllStorage()
		{
			var result = new List<Storage>();
			foreach (var node in GetNode<Node>("Buildings").GetChildren())
			{
				var building = (Building) node;
				var possible_storage = building.TryGetStorage();
				if (possible_storage is null)
				{
					continue;
				}
				result.Add(possible_storage);
			}

			return result;
		}



	}
}

