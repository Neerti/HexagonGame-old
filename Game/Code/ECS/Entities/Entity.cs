using Godot;
using Hexagon.ECS.Components;

namespace Hexagon.ECS.Entities
{
	/// <summary>
	/// An Entity represents a general object in the game, possessing no inherent functionality beyond identification.
	/// </summary>
	public class Entity
	{
		public int ID;
		public MapPositionComponent MapPosition;
	}	
}


