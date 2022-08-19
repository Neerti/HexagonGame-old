using System.Collections.Generic;
using Hexagon.Map;

namespace Hexagon.Map.MapDisplays.MapChunks
{
	/// <summary>
	/// Contains a number of <see cref="HexagonSprite"/>s that is managed by a <see cref="HexagonalGrids.HexagonalGrid"/>.
	/// As the map is moved, chunks are created and deleted in order to display more parts of the map more efficiently.
	/// <remarks>Note that this is purely for display purposes. It is not used at all for organizing actual map data.</remarks>
	/// </summary>
	public readonly struct MapDisplayChunk
	{
		public readonly VectorHex TopLeftCorner;
		public readonly VectorHex BottomRightCorner;
		public readonly List<HexagonSprite> Sprites;

		public MapDisplayChunk(VectorHex topLeftCorner, VectorHex bottomRightCorner, List<HexagonSprite> sprites)
		{
			TopLeftCorner = topLeftCorner;
			BottomRightCorner = bottomRightCorner;
			Sprites = sprites;
		}
	}
}