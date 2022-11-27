using Godot;
using Hexagon.Map.Logical.VectorHex;

namespace Hexagon.ECS.EntityGrids
{
	/// <summary>
	/// A container for entities arranged in a grid pattern, intended to store map data.
	/// Addressing specific entities can be done spatially with the <see cref="VectorHex"/> object.
	/// </summary>
	public class EntityGrid : Node
	{
		public readonly int SizeX;
		public readonly int SizeY;
		public int[,] Grid;
		
		public EntityGrid(int newSizeX, int newSizeY)
		{
			SizeX = newSizeX;
			SizeY = newSizeY;
			MakeEmptyGrid();
		}
		
		// Creates an empty grid of entity IDs. 
		// This will overwrite and completely wipe out any existing map.
		public void MakeEmptyGrid()
		{
			Grid = new int[SizeX, SizeY];
		}

		public bool IsValidCoordinate(int x, int y)
		{
			if(x < 0 || x > SizeX - 1)
			{
				return false;
			}
			if(y < 0 || y > SizeY - 1)
			{
				return false;
			}
			return true;
		}

		public int GetEntity(VectorHex hex)
		{
			return Grid[hex.X, hex.Y];
		}
	}
}


