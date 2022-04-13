using Godot;
using System;

namespace Hexagon.Calendars
{

	public class GameCalendar : Node
	{
		[Signal]
		public delegate void DayPassed();

		[Signal]
		public delegate void MonthPassed();

		[Signal]
		public delegate void YearPassed();
		
		public DateTime CurrentDateTime { get; private set; }
		public DateTime DateTimeStarted { get; private set; }

		public DateTime GetInitialDateTime()
		{
			// TODO
			return new DateTime(1, 1, 1, 8, 0, 0);
		}

		public GameCalendar()
		{
			CurrentDateTime = GetInitialDateTime();
			DateTimeStarted = CurrentDateTime;
		}

		public void AdvanceTime()
		{
			var oldDateTime = CurrentDateTime;
			CurrentDateTime = CurrentDateTime.AddHours(1);
			//CurrentDateTime = CurrentDateTime.AddDays(1);
			
			if (CurrentDateTime.Day != oldDateTime.Day)
			{
				EmitSignal(nameof(DayPassed));
			}

			if (CurrentDateTime.Month != oldDateTime.Month)
			{
				EmitSignal(nameof(MonthPassed));
			}

			if (CurrentDateTime.Year != oldDateTime.Year)
			{
				EmitSignal(nameof(YearPassed));
			}
			// TODO Make a UI to display this.
			GD.Print(CurrentDateTime.ToString("ddd MM/dd/yyyy hh:mm"));
		}
		
	}	
}
