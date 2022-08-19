using Godot;
using System;
using Hexagon.Map.MapDisplays;

namespace Hexagon.Map.SimpleMapDisplay
{
	public class HexagonGrid : YSort
	{
		public HexGrid MapToDisplay;
		private PackedScene _packedHexagonSprite;
		
		private const int SpriteWidth = 49;
		private const int SpriteHeight = 32;
		
		
		public override void _Ready()
		{
			TestMap();
			_packedHexagonSprite =
				(PackedScene) ResourceLoader.Load("res://Code/Map/MapDisplay2/HexagonSprite/HexagonSprite.tscn");
			RenderMap();
		}

		private void RenderMap()
		{
			// Not sure if it sorts once per frame or once every time the tree changes.
			// Just in case, don't do anything until the end.
			SortEnabled = false;
			
			// Make new sprites.
			for (var i = 0; i < MapToDisplay.SizeX; i++)
			{
				for (var j = 0; j < MapToDisplay.SizeY; j++)
				{
					var newSprite = (HexagonSprite)_packedHexagonSprite.Instance();
					AddChild(newSprite);

					var new_x = i * SpriteWidth;
					var new_y = j * SpriteHeight;
					if ((i & 1) == 1) // Odd numbers are moved down by half.
					{
						new_y += SpriteHeight / 2;
					}
					newSprite.Position = new Vector2(new_x, new_y);
					if (!(MapToDisplay is null))
					{
						var hex = MapToDisplay.GetHex(i, j);
						newSprite.SwitchSprite(hex.tile_type);
					}
				}
			}
			// Turning it back on appears to trigger an immediate sort, which is good.
			SortEnabled = true;
			
		}
		
		private void TestMap()
		{
			var mapGen = new MapGenerator(2);
			MapToDisplay = mapGen.GenerateNewMap(128, 128);
		}
	}
}

