using System.Collections.Generic;
using System.Diagnostics;
using Hexagon.Map;

namespace Hexagon
{
	/// <summary>
	/// Object which represents an individual hexagon, a cell for the logical hex grid.
	/// </summary>
	public class Hex
	{
		public VectorHex Position;
		
		public float Height;
		public float Humidity;
		public float Temperature;

		// If false, the player cannot see the hex at all.
		public bool Explored = false;

		// If false (and Explored is true,) the hex will be darkened.
		public bool Observed = false;

		public int CartesianBiomeBitmask = 0;
		
		public TileType tile_type = TileType.BASE;

		public enum TileType
		{
			BASE = 0,
			GRASS = 1,
			SHALLOW_SALT_WATER = 2,
			DEEP_SALT_WATER = 3,
			SHALLOW_FRESH_WATER = 4,
			DEEP_FRESH_WATER = 5,
			BEACH_SAND = 6,
			FOREST = 7,
			ROCK = 8,
			SNOW = 9
		}

		public Hex(int new_x, int new_y)
		{
			Position = new VectorHex(new_x, new_y);
		}

	}
}


