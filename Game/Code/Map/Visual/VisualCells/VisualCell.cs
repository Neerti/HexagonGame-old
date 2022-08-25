using System;
using Godot;
using Hexagon.Map.Logical.LogicalCells;
using JetBrains.Annotations;

namespace Hexagon.Map.Visual.VisualCells
{
	[UsedImplicitly]
	public class VisualCell : Sprite
	{
		private Color _baseColor = new Color(1f, 1f, 1f);
		private bool _mousedOver;
		private LogicalCell _cell;
		
		public override void _Ready()
		{
			var collision = GetNode<Area2D>("Area2D");
			collision.Connect("mouse_entered", this, nameof(OnMouseEntered));
			collision.Connect("mouse_exited", this, nameof(OnMouseExited));
		}

		public void SetUp(LogicalCell newCell)
		{
			_cell = newCell;
			SwitchSprite(_cell.TileType);
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

		private void UpdateColor()
		{
			Modulate = _mousedOver ? _baseColor.Darkened(0.2f) : _baseColor;
		}

		private void SwitchSprite(LogicalCell.TileTypes tileTypes)
		{
			switch (tileTypes)
			{
				case LogicalCell.TileTypes.Grass:
					_baseColor = new Color("#82bf40");
					break;
				
				case LogicalCell.TileTypes.Rock:
					_baseColor = new Color("#7d7d7d");
					break;
				
				case LogicalCell.TileTypes.Snow:
					_baseColor = new Color("#d6f2ff");
					break;
				
				case LogicalCell.TileTypes.Forest:
					_baseColor = new Color("#4b6f25");
					break;
				
				case LogicalCell.TileTypes.BeachSand:
					_baseColor = new Color("#dcd0c3");
					break;
				
				case LogicalCell.TileTypes.ShallowSaltWater:
					Texture = (Texture) ResourceLoader.Load(
						"res://Code/Map/Visual/VisualCells/Sprites/liquid_hexagon_64.png");
					_baseColor = new Color("#85cacc"){a = .75f};
					break;
				
				case LogicalCell.TileTypes.DeepSaltWater:
					Texture = (Texture) ResourceLoader.Load(
						"res://Code/Map/Visual/VisualCells/Sprites/liquid_hexagon_64.png");
					_baseColor = new Color("#456e95"){ a = .75f};

					break;
				
				case LogicalCell.TileTypes.ShallowFreshWater:
					_baseColor = new Color("#85b5eb");
					break;
				
				case LogicalCell.TileTypes.DeepFreshWater:
					_baseColor = new Color("#2a7ed6");
					break;
				
				case LogicalCell.TileTypes.Base:
					break;
				
				default:
					throw new ArgumentOutOfRangeException(nameof(tileTypes), tileTypes, null);
			}
			UpdateColor();
		}
	}
 
}

