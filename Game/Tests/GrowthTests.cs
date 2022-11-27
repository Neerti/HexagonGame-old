using System;
using Godot;
using Hexagon.ECS.Components.GrowthComponents;
using Hexagon.ECS.Systems;
using Xunit;

namespace Hexagon.Tests
{
	public class GrowthTests
	{
		[Fact]
		public void Growth_DayPassing_ShouldIncrement()
		{
			var component = new GrowthComponent();
			var system = new GrowthSystem();
			
			system.Grow(component, TimeSpan.FromDays(1));
			
			Assert.True(component.TimePassed > TimeSpan.Zero);
		}
	}
}