using System;
using Godot;

namespace Hexagon.Map.Visual.VisualCells
{
	public class VisualCell : Sprite
	{
		private Color _baseColor = new Color(1f, 1f, 1f);
		private bool _mousedOver = false;
		
		public override void _Ready()
		{
			var collision = GetNode<Area2D>("Area2D");
			collision.Connect("mouse_entered", this, nameof(OnMouseEntered));
			collision.Connect("mouse_exited", this, nameof(OnMouseExited));
			//collision.Connect("input_event", this, nameof(OnInputEvent));
		}

		public void OnMouseEntered()
		{
			_mousedOver = true;
			UpdateColor();
		}
		
		public void OnMouseExited()
		{
			_mousedOver = false;
			UpdateColor();
		}

		public void UpdateColor()
		{
			Modulate = _mousedOver ? _baseColor.Darkened(0.2f) : _baseColor;
		}

		public void OnInputEvent(Node thing, InputEvent @event, int otherThing)
		{
			GD.Print(@event);
		}

		public void SwitchSprite(Hex.TileType tileType)
		{
			switch (tileType)
			{
				case Hex.TileType.GRASS:
					_baseColor = new Color("#82bf40");
					break;
				
				case Hex.TileType.ROCK:
					_baseColor = new Color("#7d7d7d");
					break;
				
				case Hex.TileType.SNOW:
					_baseColor = new Color("#d6f2ff");
					break;
				
				case Hex.TileType.FOREST:
					_baseColor = new Color("#4b6f25");
					break;
				
				case Hex.TileType.BEACH_SAND:
					_baseColor = new Color("#dcd0c3");
					break;
				
				case Hex.TileType.SHALLOW_SALT_WATER:
					_baseColor = new Color("#85cacc");
					break;
				case Hex.TileType.DEEP_SALT_WATER:
					_baseColor = new Color("#456e95");
					break;
				case Hex.TileType.SHALLOW_FRESH_WATER:
					_baseColor = new Color("#85b5eb");
					break;
				case Hex.TileType.DEEP_FRESH_WATER:
					_baseColor = new Color("#2a7ed6");
					break;
				case Hex.TileType.BASE:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(tileType), tileType, null);
			}
			UpdateColor();
		}
	}
 
}

