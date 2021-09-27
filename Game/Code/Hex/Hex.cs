namespace Hexagon
{
	/// <summary>
	/// Object which represents an individual hexagon, a cell for the logical hex grid.
	/// </summary>
	public class Hex
	{
		// Cartesian coordinates.
		readonly int x, y;
		// Cube coordinates. TODO
		readonly int q, r, s;

		public float Height;
		public float Humidity;
		public float Temperature;
		
		TileType tile_type = TileType.VOID;

		enum TileType
		{
			VOID = 0,
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
			x = new_x;
			y = new_y;
		}

	}
}

