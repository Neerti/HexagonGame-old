using System;

namespace Hexagon.ECS.Components.GrowthComponents
{
	public class GrowthComponent : Component
	{
		public TimeSpan TimeUntilGrown;
		
		public TimeSpan TimePassed;
	}
}