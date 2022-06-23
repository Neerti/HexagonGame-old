using Godot;
using Hexagon.Calendars;

namespace Hexagon.Main
{
	public class Main : Node
	{
		public override void _Ready()
		{
			// Connect signals together.
			var calendar = GetNode<GameCalendar>("Time/GameCalendar");
			var factions = GetNode<AllFactions>("AllFactions");

			calendar.Connect(nameof(GameCalendar.DayPassed), factions, nameof(AllFactions.ProcessFactions));
		}
	}  
}

