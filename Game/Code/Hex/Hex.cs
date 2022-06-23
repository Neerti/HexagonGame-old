using System.Collections.Generic;
using System.Diagnostics;

namespace Hexagon
{
	/// <summary>
	/// Object which represents an individual hexagon, a cell for the logical hex grid.
	/// </summary>
	public class Hex
	{
		// Cartesian coordinates.
		public readonly int X, Y;
		// Cube coordinates.
		public readonly int Q, R, S;

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
			X = new_x;
			Y = new_y;
			
			// Calculate cube coordinates.
			/*

function oddq_to_cube(hex):
    var q = hex.col
    var r = hex.row - (hex.col - (hex.col&1)) / 2
    return Cube(q, r, -q-r)
*/
			
			Q = X;
			R = Y - (X - (X & 1)) / 2;
			S = -Q - R;
			
			Debug.Assert(Q + R + S == 0);

		}

	}
}


