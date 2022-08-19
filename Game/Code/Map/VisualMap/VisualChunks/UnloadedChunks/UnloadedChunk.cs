using Godot;

namespace Hexagon.Map.VisualMap.VisualChunks.UnloadedChunks
{
	public class UnloadedChunk : VisualChunk
	{
		public override void OnScreenEntered()
		{
			GD.Print($"Loading chunk {ChunkX}, {ChunkY}.");
			EmitSignal(nameof(OnChunkVisible), this);
		}
	}	
}

