using Godot;
using System;

namespace Hexagon.Map.SimpleMapDisplay
{
	public class MapCamera : Camera2D
	{
		private const float MapMovementSpeed = 400f;

		public override void _Process(float delta)
		{
			var direction = new Vector2();
			var speed = MapMovementSpeed / Scale.x;
			if(Input.IsActionPressed("move_map_up"))
			{
				direction += Vector2.Up;
			}
			if (Input.IsActionPressed("move_map_down"))
			{
				direction += Vector2.Down;
			}
			if (Input.IsActionPressed("move_map_left"))
			{
				direction += Vector2.Left;
			}
			if (Input.IsActionPressed("move_map_right"))
			{
				direction += Vector2.Right;
			}

			// Holding shift makes the map move faster.
			if (Input.IsPhysicalKeyPressed((int) KeyList.Shift))
			{
				speed *= 2;
			}
			
			Position += (direction.Normalized() * speed * delta);
		}


	}
}

