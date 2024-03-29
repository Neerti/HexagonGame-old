using System;
using Godot;
using Hexagon.Map.Logical.LogicalCells;
using Hexagon.Map.Logical.Terrains;
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
			SwitchSprite(_cell.Terrain);
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

		private void SwitchSprite(Terrain terrain)
		{
			_baseColor = terrain.TileColor;
			Texture = ResourceLoader.Load<Texture>(terrain.TileTexturePath);
			UpdateColor();
		}
		
	}
 
}

