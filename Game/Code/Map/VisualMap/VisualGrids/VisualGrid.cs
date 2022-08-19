using Godot;
using Hexagon.Map.VisualMap.VisualChunks;
using Hexagon.Map.VisualMap.VisualChunks.HybridChunks;
using Hexagon.Map.VisualMap.VisualChunks.LoadedChunks;
using Hexagon.Map.VisualMap.VisualChunks.UnloadedChunks;

namespace Hexagon.Map.VisualMap.VisualGrids
{
	public class VisualGrid : Node2D
	{
		private HexGrid Map;
		private PackedScene _packedUnloadedChunk;
		private PackedScene _packedLoadedChunk;

		public override void _Ready()
		{
			var mapGen = new MapGenerator(2);
			Map = mapGen.GenerateNewMap(512, 512);
			
			BuildGrid();
		}
		
		public void BuildGrid()
		{
			_packedLoadedChunk = 
				(PackedScene) ResourceLoader.Load("res://Code/Map/VisualMap/VisualChunks/LoadedChunks/LoadedChunk.tscn");
			_packedUnloadedChunk =
				(PackedScene) ResourceLoader.Load("res://Code/Map/VisualMap/VisualChunks/UnloadedChunks/UnloadedChunk.tscn");
			var _packedChunk = (PackedScene) ResourceLoader.Load("res://Code/Map/VisualMap/VisualChunks/HybridChunks/HybridChunk.tscn");
			
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
					
					//GetNode<YSort>("Chunks").AddChild(newUnloadedChunk);
					GetNode<YSort>("Chunks").AddChild(newChunk);
				}
			}
		}

		// TODO: Reduce copypasta here and below.
		public UnloadedChunk MakeUnloadedChunk(HexGrid map, int newChunkX, int newChunkY)
		{
			var newUnloadedChunk = (UnloadedChunk) _packedUnloadedChunk.Instance();
			
			newUnloadedChunk.Position = new Vector2(
				newChunkX * VisualChunk.ChunkWidth,
				newChunkY * VisualChunk.ChunkHeight
			);
			
			newUnloadedChunk.SetUp(Map, newChunkX, newChunkY);
			
			newUnloadedChunk.Connect(
				nameof(VisualChunk.OnChunkVisible),
				this,
				nameof(UnloadedToLoaded)
			);

			return newUnloadedChunk;
		}
		
		public LoadedChunk MakeLoadedChunk(HexGrid map, int newChunkX, int newChunkY)
		{
			var newLoadedChunk = (LoadedChunk) _packedLoadedChunk.Instance();
			
			newLoadedChunk.Position = new Vector2(
				newChunkX * VisualChunk.ChunkWidth,
				newChunkY * VisualChunk.ChunkHeight
			);
			
			newLoadedChunk.SetUp(Map, newChunkX, newChunkY);
			var time_started = Time.GetTicksMsec();
			newLoadedChunk.LoadChunk(Map, newChunkX, newChunkY);
			var time_ended = Time.GetTicksMsec();
			GD.Print($"LoadChunk() took {time_ended - time_started} milliseconds.");
			
			newLoadedChunk.Connect(
				nameof(VisualChunk.OnChunkInvisible),
				this,
				nameof(LoadedToUnloaded)
			);

			return newLoadedChunk;
		}

		public void UnloadedToLoaded(UnloadedChunk source)
		{
			GD.Print($"Going to load chunk {source.ChunkX}, {source.ChunkY}.");
			var newLoadedChunk = MakeLoadedChunk(source.Map, source.ChunkX, source.ChunkY);
			//GetNode<YSort>("Chunks").CallDeferred("add_child", newLoadedChunk);
			var time_started = Time.GetTicksMsec();
			GetNode<YSort>("Chunks").AddChild(newLoadedChunk);
			var time_ended = Time.GetTicksMsec();
			GD.Print($"AddChild() took {time_ended - time_started} milliseconds.");
			source.QueueFree();
		}

		public void LoadedToUnloaded(LoadedChunk source)
		{
			GD.Print($"Going to unload chunk {source.ChunkX}, {source.ChunkY}.");
			var newUnloadedChunk = MakeUnloadedChunk(source.Map, source.ChunkX, source.ChunkY);
			GetNode<YSort>("Chunks").CallDeferred("add_child", newUnloadedChunk);
			source.QueueFree();
		}


	}
}

