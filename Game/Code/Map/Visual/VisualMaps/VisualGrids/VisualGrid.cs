using Godot;
using Hexagon.Map.VisualMap.VisualChunks;
using Hexagon.Map.VisualMap.VisualChunks.HybridChunks;

namespace Hexagon.Map.VisualMap.VisualGrids
{
	public class VisualGrid : Node2D
	{
		private HexGrid Map;

		public override void _Ready()
		{
			var mapGen = new MapGenerator(2);
			Map = mapGen.GenerateNewMap(512, 512);
			
			BuildGrid();
		}
		
		public void BuildGrid()
		{
			var _packedChunk = (PackedScene) ResourceLoader.Load("res://Code/Map/Visual/VisualMaps/VisualChunks/HybridChunks/HybridChunk.tscn");
			
			for (int i = 0; i < Map.SizeX / VisualChunks.VisualChunk.ChunkSize; i++)
			{
				for (int j = 0; j < Map.SizeY / VisualChunks.VisualChunk.ChunkSize; j++)
				{
					//var newUnloadedChunk = MakeUnloadedChunk(Map, i, j);
					var newChunk = (HybridChunk)_packedChunk.Instance();

					newChunk.Name = $"Chunk {i},{j}";
					
					newChunk.Position = new Vector2(
						i * VisualChunk.ChunkWidth,
						j * VisualChunk.ChunkHeight
					);
					
					newChunk.SetUp(Map, i, j);
					
					GetNode<YSort>("Chunks").AddChild(newChunk);
				}
			}
		}

	}
}

