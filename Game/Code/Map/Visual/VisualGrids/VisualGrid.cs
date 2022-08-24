using Godot;
using Hexagon.Map.Visual.VisualChunks;
using Hexagon.Map.Visual.VisualChunks.HybridChunks;

namespace Hexagon.Map.Visual.VisualGrids
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
			var _packedChunk = (PackedScene) ResourceLoader.Load("res://Code/Map/Visual/VisualChunks/HybridChunks/HybridChunk.tscn");
			
			for (int i = 0; i < Map.SizeX / VisualChunk.ChunkSize; i++)
			{
				for (int j = 0; j < Map.SizeY / VisualChunk.ChunkSize; j++)
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

