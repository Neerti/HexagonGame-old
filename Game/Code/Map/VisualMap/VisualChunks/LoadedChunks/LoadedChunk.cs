using Godot;

namespace Hexagon.Map.VisualMap.VisualChunks.LoadedChunks
{

	public class LoadedChunk : VisualChunk
	{
		
		public void LoadChunk(HexGrid map, int newChunkX, int newChunkY)
		{
			// Make the sprites.
			var spriteGrid = GetNode<SpriteGrid>("SpriteGrid");
			spriteGrid.DrawChunk(Map, ChunkX, ChunkY);
		}

		public override void OnScreenExited()
		{
			GD.Print($"Unloaded chunk {ChunkX}, {ChunkY}.");
			EmitSignal(nameof(OnChunkInvisible), this);
		}
	}
}


