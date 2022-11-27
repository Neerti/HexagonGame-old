using System.Collections.Generic;
using System.Diagnostics;
using Godot;
using Hexagon.ECS.Components;
using Hexagon.ECS.SparseSets;

namespace Hexagon.ECS.Worlds
{
	/// <summary>
	/// The World object holds every entity and component in the game that currently exists.
	/// Consequently, it holds all mutable state for a particular save game, and is the starting
	/// point for serialization and deserialization.
	/// </summary>
	public class World : Node
	{
		public List<int> Entities = new List<int>();
		public int EntityTally;

		// In the future, this should be some kind of object to manage this for each component.
		public SparseSet<TestComponent> TestComponents;

		public int NewEntity()
		{
			Entities.Add(EntityTally);
			return EntityTally++;
		}

		public override void _Ready()
		{
			// Test code to simulate a (somewhat) worst case scenario with massive amounts of entities.

			var timer = new Stopwatch();
			timer.Start();

			const int entitiesToMake = 1000000;
			TestComponents = new SparseSet<TestComponent>((int)(entitiesToMake * 1.5f)); // Let's be dumb on purpose.

			// Make entities with one component, that holds an integer.
			// Making an object to handle adding/removing components from entities later on is probably a good idea.
			for (var i = 0; i < entitiesToMake; i++)
			{
				var newEntity = NewEntity();
				var newComponent = new TestComponent();
				newComponent.number = i;
				TestComponents.Add(newEntity, newComponent);
			}
			
			timer.Stop();
			GD.Print($"Made {TestComponents.Count} components.");
			GD.Print($"{timer.ElapsedMilliseconds} ms {timer.ElapsedMilliseconds} to create {Entities.Count} entities.");
			
			// Memory usage stuff.
			var currentProcess = Process.GetCurrentProcess();
			var totalBytesOfMemoryUsed = currentProcess.WorkingSet64;
			GD.Print($"{totalBytesOfMemoryUsed / 1024 / 1024} MiB of memory used.");
			
			timer.Reset();
			// Let's see how long it takes to add some numbers together, with pretty syntax.
			// timer.Start();
			// foreach (var component in TestComponents)
			// {
			// 	component.number += 5;
			// }
			//
			// timer.Stop();
			// GD.Print($"{timer.ElapsedMilliseconds} ms to edit {Entities.Count} entities with foreach().");
			//
			// timer.Reset();
			// Do it again but with less pretty but probably faster syntax.
			timer.Start();
			for (var i = 0; i < TestComponents.Count; i++)
			{
				var component = TestComponents.Elements[i];
				component.number += 5;
			}
			timer.Stop();
			GD.Print($"{timer.ElapsedMilliseconds} ms to edit {Entities.Count} entities with for() and mutating the component.");
			
			timer.Reset();
			timer.Start();
			for (var i = 0; i < TestComponents.Count; i++)
			{
				var component = TestComponents.Elements[i];
				TestComponents.Elements[i] = new TestComponent{ number = i + 5};
			}
			timer.Stop();
			GD.Print($"{timer.ElapsedMilliseconds} ms to edit {Entities.Count} entities with for() and replacing " +
			         $"the component.");
			
		}
		
	}
}


