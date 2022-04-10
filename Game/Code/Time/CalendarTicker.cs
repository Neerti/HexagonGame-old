using Godot;
using System;
using System.Collections.Generic;

namespace Hexagon.Calendars
{
	public class CalendarTicker : Timer
	{
		[Signal]
		public delegate void PauseToggled(bool setPaused);

		[Signal]
		public delegate void GameSpeedChanged(int oldIndex, int newIndex);
		
		public int SpeedIndex { get; private set; }
		private readonly List<float> _speedOptions = new List<float> { 1f, 0.5f, 0.25f, 0.1f, 0.01f };
		
		public override void _Ready()
		{
			PauseGame(true); // Start out paused.
			SetSpeed(SpeedIndex);
			Start();
			Connect("timeout", GetParent<GameCalendar>(), nameof(GameCalendar.AdvanceTime));

		}
		
		public override void _Input(InputEvent @event)
		{
			if(@event.IsActionReleased("toggle_pause"))
			{
				PauseGame(!Paused);
			}
			else if (@event.IsActionReleased("increase_speed"))
			{
				SetSpeed(SpeedIndex + 1);
			}
			else if(@event.IsActionReleased("decrease_speed"))
			{
				SetSpeed(SpeedIndex - 1);
			}
		}
		
		/// <summary>
		/// Pauses or unpauses the timer, which in turn controls whether the rest of the game is paused.
		/// </summary>
		/// <param name="setPaused">Pass True to pause the game, false to unpause.</param>
		public void PauseGame(bool setPaused)
		{
			Paused = setPaused;
			EmitSignal(nameof(PauseToggled), setPaused);
		}

		/// <summary>
		/// Sets the timer to one of possible_speeds by index.
		/// </summary>
		/// <param name="newSpeedIndex">Desired speed for the timer. 
		/// Automatically clamped, so it is safe to do 'speed_index+1' even 
		/// on max speed.</param>
		public void SetSpeed(int newSpeedIndex)
		{
			int oldSpeedIndex = SpeedIndex;
			SpeedIndex = Mathf.Clamp(newSpeedIndex, 0, _speedOptions.Count - 1);
			WaitTime = _speedOptions[SpeedIndex];
			if (oldSpeedIndex == SpeedIndex) return;
			Start(); // To reset the delay, and feel more responsive.
			EmitSignal(nameof(GameSpeedChanged), oldSpeedIndex, SpeedIndex);

		}
		
		
	}
	
	
}



