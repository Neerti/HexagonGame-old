using Hexagon.Map.Logical.Terrains;

namespace Hexagon.Map.Logical.LogicalCells
{
	/// <summary>
	/// Object which represents an individual hexagon, a cell for the logical hex grid.
	/// </summary>
	public class LogicalCell
	{
		public VectorHex.VectorHex Position;
		
		public float Height;
		public float Humidity;
		public float Temperature;
		
		/// <summary>
		/// If false, the player cannot see the hex at all.
		/// This is not implemented.
		/// </summary>
		public bool Explored = false;
		
		/// <summary>
		/// If false, and <see cref="Explored"/> is true, the hex will be darkened.
		/// This is not implemented.
		/// </summary>
		public bool Observed = false;

		/// <summary>
		/// Reference to one of the <see cref="Terrain"/> singletons that this cell is.
		/// </summary>
		public Terrain Terrain;

		public LogicalCell(int new_x, int new_y)
		{
			Position = new VectorHex.VectorHex(new_x, new_y);
		}

	}
}


