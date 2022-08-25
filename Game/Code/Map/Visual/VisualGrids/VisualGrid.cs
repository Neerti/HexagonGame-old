using Godot;
using Hexagon.Map.Visual.VisualChunks;
using Hexagon.Map.Visual.VisualChunks.HybridChunks;

namespace Hexagon.Map.Visual.VisualGrids
{
	public class VisualGrid : Node2D
	{
		private LogicalGrid _map;

		public override void _Ready()
		{
			var mapGen = new MapGenerator(2);
			_map = mapGen.GenerateNewMap(512, 512);
			
			BuildGrid();
		}

		private void BuildGrid()
		{
			var packedChunk = (PackedScene) ResourceLoader.Load("res://Code/Map/Visual/VisualChunks/HybridChunks/HybridChunk.tscn");
			
			for (var i = 0; i < _map.SizeX / VisualChunk.ChunkSize; i++)
			{
				for (var j = 0; j < _map.SizeY / VisualChunk.ChunkSize; j++)
				{
					var newChunk = (HybridChunk)packedChunk.Instance();

					newChunk.Name = $"Chunk {i},{j}";
					
					newChunk.Position = new Vector2(
						i * VisualChunk.ChunkWidth,
						j * VisualChunk.ChunkHeight
					);
					
					newChunk.SetUp(_map, i, j);
					
					GetNode<YSort>("Chunks").AddChild(newChunk);
				}
			}
		}

	}
}

