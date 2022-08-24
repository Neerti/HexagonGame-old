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
		
		public TileTypes TileType = TileTypes.Base;

		public enum TileTypes
		{
			Base = 0,
			Grass = 1,
			ShallowSaltWater = 2,
			DeepSaltWater = 3,
			ShallowFreshWater = 4,
			DeepFreshWater = 5,
			BeachSand = 6,
			Forest = 7,
			Rock = 8,
			Snow = 9
		}

		public Hex(int new_x, int new_y)
		{
			Position = new VectorHex(new_x, new_y);
		}

	}
}


