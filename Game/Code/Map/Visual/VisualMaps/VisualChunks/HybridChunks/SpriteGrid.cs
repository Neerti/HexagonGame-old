using Godot;
using Hexagon.Map.MapDisplays;

namespace Hexagon.Map.VisualMap.VisualChunks.LoadedChunks
{
	public class SpriteGrid : YSort
	{
		private PackedScene _packedHexagonSprite;

		public void DrawChunk(HexGrid map, int chunkX, int chunkY)
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

			if (_packedHexagonSprite is null)
			{
				_packedHexagonSprite =
					(PackedScene) ResourceLoader.Load("res://Code/Map/MapDisplay2/HexagonSprite/HexagonSprite.tscn");
			}

			// Make new sprites.
			for (var i = 0; i < VisualChunk.ChunkSize; i++)
			{
				for (var j = 0; j < VisualChunk.ChunkSize; j++)
				{
					var newSprite = (HexagonSprite)_packedHexagonSprite.Instance();
					AddChild(newSprite);

					var new_x = i * VisualChunk.SpriteWidth;
					var new_y = j * VisualChunk.SpriteHeight;
					if ((i & 1) == 1) // Odd numbers are moved down by half.
					{
						new_y += VisualChunk.SpriteHeight / 2;
					}
					newSprite.Position = new Vector2(new_x, new_y);
					if (map is null) continue;
					
					var hex = map.GetHex(
						VisualChunk.ChunkSize * chunkX + i,
						VisualChunk.ChunkSize * chunkY + j
					);
					newSprite.SwitchSprite(hex.tile_type);
				}
			}
			// Turning it back on appears to trigger an immediate sort, which is good.
			SortEnabled = true;
		}
	}
}

