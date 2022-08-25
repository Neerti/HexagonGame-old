using System;
using Godot;
using Hexagon.Globals;
using Hexagon.Map.Logical.LogicalCells;
using Hexagon.Map.Logical.Terrains;
using Hexagon.Map.Logical.VectorHex;

namespace Hexagon
{
	/// <summary>
	/// A container for logical hexagon objects.
	/// </summary>
	public class LogicalGrid
	{
		public readonly int SizeX;
		public readonly int SizeY;
		private LogicalCell[,] _grid;

		public LogicalGrid(int new_size_x, int new_size_y)
		{
			SizeX = new_size_x;
			SizeY = new_size_y;
			MakeEmptyGrid();
		}

		public LogicalCell GetHex(int x, int y)
		{
			if(x < 0 || x > SizeX - 1)
			{
				throw new IndexOutOfRangeException("X coordinate was out of bounds.");
			}
			if(y < 0 || y > SizeY - 1)
			{
				throw new IndexOutOfRangeException("Y coordinate was out of bounds.");
			}
			return _grid[x,y];
		}

		public LogicalCell GetHex(VectorHex v)
		{
			return GetHex(v.X, v.Y);
		}

		public bool HasHex(int x, int y)
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

		public bool HasHex(VectorHex v)
		{
			return HasHex(v.X, v.Y);
		}

		public LogicalCell GetHexByOffset(int x, int y, int x_offset, int y_offset)
		{
			if(HasHex(x + x_offset, y + y_offset))
			{
				return GetHex(x + x_offset, y + y_offset);
			}
			return null;
		}
		
		public LogicalCell GetHexByOffset(LogicalCell tile, int x_offset, int y_offset)
		{
			return GetHexByOffset(tile.Position.X, tile.Position.Y, x_offset, y_offset);
		}


		// Creates an empty grid of logical hexagons objects. 
		// This will overwrite and completely wipe out any existing map.
		private void MakeEmptyGrid()
		{
			_grid = new LogicalCell[SizeX, SizeY];
			for (var x = 0; x < SizeX; x++)
			{
				for (var y = 0; y < SizeY; y++)
				{
					_grid[x, y] = new LogicalCell(x, y);
				}
			}
		}

		public void AssignBiomesToHexes()
		{
			for(var x = 0; x < SizeX; x++)
			{
				for(var y = 0; y < SizeY; y++)
				{
					// For now it's just height based.
					// Someday it will also incorporate temperature and humidity.

					var tile = _grid[x,y];
					var value = tile.Height;

					// TESTING
					var chosen_terrain_type = TerrainKinds.Base;

					// This is super ugly and hopefully temporary.
					const float seaLevelOffset = 0.1f;
					if(value > (0.25f + seaLevelOffset))
					{
						chosen_terrain_type = TerrainKinds.Snow;
					}
					else if(value > (0.20f + seaLevelOffset))
					{
						chosen_terrain_type = TerrainKinds.Rock;
					}
					else if(value > (0.15f + seaLevelOffset))
					{
						chosen_terrain_type = TerrainKinds.Forest;
					}
					else if(value > (0.05f + seaLevelOffset))
					{
						chosen_terrain_type = TerrainKinds.Grass;
					}
					else if(value > (0f + seaLevelOffset))
					{
						chosen_terrain_type = TerrainKinds.BeachSand;
					}
					else if(value > (-0.15f + seaLevelOffset))
					{
						chosen_terrain_type = TerrainKinds.ShallowSaltWater;
					}
					else
					{
						chosen_terrain_type = TerrainKinds.DeepSaltWater;
					}
					tile.Terrain = Singleton.AllTerrains[chosen_terrain_type];
				}
			}
		}

	}
	
}


