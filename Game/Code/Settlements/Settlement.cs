using System.Collections.Generic;
using Godot;
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
		public List<Person> People;

		public List<Building> Buildings;




	}
}

