using Godot;
using System.Collections.Generic;
using Hexagon.ECS.Entities;

namespace Hexagon.Worlds
{
	/// <summary>
	/// The World object holds every <see cref="Entity"/> in the game that currently exists.
	/// Consequently, it holds all mutable state for a particular save game, and is the starting
	/// point for serialization and deserialization.
	/// </summary>
	public class World : Node
	{
		public Dictionary<int, Entity> Entities;
		public int EntityTally;

		public Entity NewEntity()
		{
			var entity = new Entity();
			entity.ID = EntityTally;
			Entities.Add(EntityTally++, entity);
			return entity;
		}
	}
}


