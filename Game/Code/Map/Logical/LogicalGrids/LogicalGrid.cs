using System;
using Godot;
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

		public Image GenerateHeightNoiseImage()
		{
			var img = new Image();
			img.Create(SizeX, SizeY, false, Image.Format.Rgba8);
			img.Lock();
			for(var x = 0; x < SizeX; x++)
			{
				for(var y = 0; y < SizeY; y++)
				{
					float value = _grid[x,y].Height;

					var chosen_color = Colors.Black;
					chosen_color = chosen_color.LinearInterpolate(Colors.White, value);
					img.SetPixel(x, y, chosen_color);
				}
			}
			img.Unlock();
			return img;
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
					float value = tile.Height;

					// TESTING
					var chosen_tile_type = LogicalCell.TileTypes.Base;

					// This is super ugly and hopefully temporary.
					float sea_level_offset = 0.1f;
					if(value > (0.25f + sea_level_offset))
					{
						chosen_tile_type = LogicalCell.TileTypes.Snow;
					}
					else if(value > (0.20f + sea_level_offset))
					{
						chosen_tile_type = LogicalCell.TileTypes.Rock;
					}
					else if(value > (0.15f + sea_level_offset))
					{
						chosen_tile_type = LogicalCell.TileTypes.Forest;
					}
					else if(value > (0.05f + sea_level_offset))
					{
						chosen_tile_type = LogicalCell.TileTypes.Grass;
					}
					else if(value > (0f + sea_level_offset))
					{
						chosen_tile_type = LogicalCell.TileTypes.BeachSand;
					}
					else if(value > (-0.15f + sea_level_offset))
					{
						chosen_tile_type = LogicalCell.TileTypes.ShallowSaltWater;
					}
					else
					{
						chosen_tile_type = LogicalCell.TileTypes.DeepSaltWater;
					}
					tile.TileType = chosen_tile_type;
				}
			}
		}

		public Image GenerateMapImage()
		{
			var img = new Image();
			img.Create(SizeX, SizeY, false, Image.Format.Rgba8);
			img.Lock();

			for(var x = 0; x < SizeX; x++)
			{
				for(var y = 0; y < SizeY; y++)
				{
					var chosen_color = Colors.White;
					var tile = _grid[x,y];
					// TileTypes should probably be made into their own objects.
					switch (tile.TileType)
					{
						case LogicalCell.TileTypes.Snow:
							chosen_color = Colors.LightBlue;
							break;
						case LogicalCell.TileTypes.Rock:
							chosen_color = Colors.DimGray;
							break;
						case LogicalCell.TileTypes.Forest:
							chosen_color = Colors.DarkGreen;
							break;
						case LogicalCell.TileTypes.Grass:
							chosen_color = Colors.Limegreen;
							break;
						case LogicalCell.TileTypes.BeachSand:
							chosen_color = Colors.PaleGoldenrod;
							break;
						case LogicalCell.TileTypes.ShallowSaltWater:
							chosen_color = Colors.MediumBlue;
							break;
						case LogicalCell.TileTypes.DeepSaltWater:
							chosen_color = Colors.NavyBlue;
							break;	
						default:
							chosen_color = Colors.Black;
							break;
					}

					if(GetHexBiomeBitmask(tile) != 15)
					{
						chosen_color = chosen_color.Darkened(0.5f);
					}

					img.SetPixel(x, y, chosen_color);
				}
			}
			img.Unlock();
			return img;
		}

		public Image GenerateHeatMap()
		{
			var img = new Image();
			img.Create(SizeX, SizeY, false, Image.Format.Rgba8);
			img.Lock();

			for(var x = 0; x < SizeX; x++)
			{
				for(var y = 0; y < SizeY; y++)
				{
					var chosen_color = Colors.Blue;
					var tile = _grid[x,y];
					chosen_color = chosen_color.LinearInterpolate(Colors.Red, tile.Temperature);

					if(GetHexBiomeBitmask(tile) != 15)
					{
						chosen_color = chosen_color.Darkened(0.5f);
					}
					img.SetPixel(x, y, chosen_color);
					
				}
			}
			img.Unlock();
			return img;
		}

		public int GetHexBiomeBitmask(LogicalCell tile)
		{
			int result = 0;
			LogicalCell top_tile, bottom_tile, left_tile, right_tile;
			top_tile = GetHexByOffset(tile,  0, -1);
			bottom_tile = GetHexByOffset(tile,  0,  1);
			left_tile =  GetHexByOffset(tile, -1,  0);
			right_tile =  GetHexByOffset(tile,  1,  0);
			if(top_tile?.TileType == tile.TileType)
			{
				result += 1;
			}
			if(right_tile?.TileType == tile.TileType)
			{
				result += 2;
			}
			if(bottom_tile?.TileType == tile.TileType)
			{
				result += 4;
			}
			if(left_tile?.TileType == tile.TileType)
			{
				result += 8;
			}
			return result;
		}

	}
	
}


