using Godot;
using JetBrains.Annotations;

namespace Hexagon.Map.Visual.VisualScreens
{
	// Click-drag code adapted from code written by "AlienBuchner" at https://godotengine.org/qa/46892/would-map-navigation-camera-with-middle-mouse#a52576.
	[UsedImplicitly]
	public class VisualScreen : Camera2D
	{
		private const float BaseMovementSpeed = 400f;
		private Vector2 _fixedTogglePoint;

		public override void _PhysicsProcess(float delta)
		{
			if (Input.IsActionJustPressed("drag_map"))
			{
				Input.SetDefaultCursorShape(Input.CursorShape.Drag);
				var mousePosition = GetViewport().GetMousePosition();
				_fixedTogglePoint = mousePosition;
			}

			if (Input.IsActionJustReleased("drag_map"))
			{
				// Reset cursor to normal shape.
				Input.SetDefaultCursorShape();
			}
			if (Input.IsActionPressed("drag_map"))
			{
				DragMapAround();
			}
			else
			{
				var direction = new Vector2();
				var speed = BaseMovementSpeed / Scale.x;
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
			
				Position += direction.Normalized() * speed * delta;
			}
			
		}
		
		private void DragMapAround()
		{
			var mousePosition = GetViewport().GetMousePosition();
			var newPosition = new Vector2(
				GlobalPosition.x - (mousePosition.x - _fixedTogglePoint.x),
				GlobalPosition.y - (mousePosition.y - _fixedTogglePoint.y)
				);
			GlobalPosition = newPosition;
			_fixedTogglePoint = mousePosition;
		}
	}
}
