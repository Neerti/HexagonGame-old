using System;
using System.Collections.Generic;
using Godot;
using Hexagon.ECS.Components.GrowthComponents;

namespace Hexagon.ECS.Systems
{
	public class GrowthSystem : EntitySystem
	{
		[Signal]
		public delegate void FinishedGrowing(GrowthComponent component);
		
		public void OnDayPassed()
		{
			var list = GetTree().GetNodesInGroup("GrowthComponents");
			foreach (var thing in list)
			{
				var component = (GrowthComponent) thing;
				Grow(component, TimeSpan.FromDays(1));
			}
		}	
		
		public void Grow(GrowthComponent component, TimeSpan amount)
		{
			component.TimePassed += amount;

			if (component.TimePassed >= component.TimeUntilGrown)
			{
				EmitSignal(nameof(FinishedGrowing), component);
			}
		}

		// This might be more performant but it should be profiled to see if that's true.
		public void GrowAllInList(List<GrowthComponent> list, TimeSpan amount)
		{
			for (var i = 0; i < list.Count; i++)
			{
				var component = list[i];
				component.TimePassed += amount;
				if (component.TimePassed >= component.TimeUntilGrown)
				{
					EmitSignal(nameof(FinishedGrowing), component);
				}
			}
		}
	}
}