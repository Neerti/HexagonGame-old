using Godot;
using System;

namespace Hexagon.Map.MapDisplays.HexagonalGrids
{
	/// <summary>
	/// Contains a number of <see cref="HexagonSprite"/>s that is managed by a <see cref="HexagonalGrids.HexagonalGrid"/>.
	/// As the map is moved, chunks are created and deleted in order to display more parts of the map more efficiently.
	/// <remarks>Note that this is purely for display purposes. It is not used at all for organizing actual map data.</remarks>
	/// </summary>
	public class Chunk : YSort
	{

		private PackedScene _packedHexagonSprite;
		
		private const int SpriteWidth = 49;
		private const int SpriteHeight = 32;
		/// <summary>
		/// Length of a single chunk, horizontally and vertically.
		/// </summary>
		public const int ChunkSize = 8;

		public static int ChunkWidth = SpriteWidth * ChunkSize;
		public static int ChunkHeight = SpriteHeight * ChunkSize;

		private VectorHex _originCoordinate;
		private HexGrid _map;

		public void SetUp(VectorHex newCoords, HexGrid newMap)
		{
			_originCoordinate = newCoords;
			_map = newMap;
			
			_packedHexagonSprite =
				(PackedScene) ResourceLoader.Load("res://Code/Map/MapDisplay2/HexagonSprite/HexagonSprite.tscn");
			
			DrawChunk();
		}

		public void DrawChunk()
		{
			// Not sure if it sorts once per frame or once every time the tree changes.
			// Just in case, don't do anything until the end.
			SortEnabled = false;
			
			// Delete all the sprites.
			foreach (var child in GetChildren())
			{
				var node = (Node)child;
				node.QueueFree();
			}
			
			// Make new sprites.
			for (var i = 0; i < ChunkSize; i++)
			{
				for (var j = 0; j < ChunkSize; j++)
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
					if (!(_map is null))
					{
						var hex = _map.GetHex(_originCoordinate.X + i, _originCoordinate.Y + j);
						newSprite.SwitchSprite(hex.tile_type);
					}
				}
			}
			// Turning it back on appears to trigger an immediate sort, which is good.
			SortEnabled = true;
		}

	}  
}



