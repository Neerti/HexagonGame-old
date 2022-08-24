using Godot;

namespace Hexagon.Map.Visual.VisualChunks
{
	/// <summary>
	/// Base class for chunks that react to being on or off screen.
	/// </summary>
	public class VisualChunk : Node2D
	{
		/// <summary>
		/// Cartesian X coordinate for this chunk instance.
		/// </summary>
		public int ChunkX { get; protected set; }

		/// <summary>
		/// Cartesian Y coordinate for this chunk instance.
		/// </summary>
		public int ChunkY { get; protected set; }
		
		public HexGrid Map { get; protected set; }
		
		/// <summary>
		/// Assumed width of the individual sprite inside of a chunk, used to tile the sprites correctly.
		/// Note that this may not be the sprite's true size, in order to allow for overlapping.
		/// </summary>
		public static int SpriteWidth = 49;
		
		/// <summary>
		/// Assumed height of the individual sprite inside of a chunk, used to tile the sprites correctly.
		/// Note that this may not be the sprite's true size, in order to allow for overlapping.
		/// </summary>
		public static int SpriteHeight = 32;
		
		/// <summary>
		/// How many columns and rows of sprites are in a chunk, horizontally and vertically.
		/// </summary>
		public static int ChunkSize = 16;
		
		/// <summary>
		/// Approximate total width of the chunk, in pixels.
		/// </summary>
		public static int ChunkWidth = SpriteWidth * ChunkSize;
		
		/// <summary>
		/// Approximate total height of the chunk, in pixels.
		/// </summary>
		public static int ChunkHeight = SpriteHeight * ChunkSize;

		/// <summary>
		/// Width of the boundary for
		/// </summary>
		public static int ChunkVisibleWidth = ChunkWidth + (SpriteWidth * 2);
		
		public static int ChunkVisibleHeight = ChunkHeight + (SpriteHeight * 3);

		public void SetUp(HexGrid map, int newChunkX, int newChunkY)
		{
			Map = map;
			ChunkX = newChunkX;
			ChunkY = newChunkY;
		}

		public virtual void OnScreenEntered() { }
		
		public virtual void OnScreenExited() { }
	}
}


