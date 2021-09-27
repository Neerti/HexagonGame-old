using System;
using Godot;

namespace Hexagon
{
	/// <summary>
	/// A container for logical hexagon objects.
	/// </summary>
	public class HexGrid
	{
		readonly public int SizeX;
		readonly public int SizeY;
		private Hex[,] grid;

		public HexGrid(int new_size_x, int new_size_y)
		{
			SizeX = new_size_x;
			SizeY = new_size_y;
			MakeEmptyGrid();
		}

		public Hex GetHex(int x, int y)
		{
			if(x < 0 || x > SizeX)
			{
				throw new IndexOutOfRangeException("X coordinate was out of bounds.");
			}
			if(y < 0 || y > SizeY)
			{
				throw new IndexOutOfRangeException("Y coordinate was out of bounds.");
			}
			return grid[x,y];
		}

		// Creates an empty grid of logical hexagons objects. 
		// This will overwrite and completely wipe out any existing map.
		private void MakeEmptyGrid()
		{
			grid = new Hex[SizeX, SizeY];
			for (int x = 0; x < SizeX; x++)
			{
				for (int y = 0; y < SizeY; y++)
				{
					grid[x, y] = new Hex(x, y);
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
					float value = grid[x,y].Height;

					var chosen_color = Colors.White;
					// This is superugly and hopefully temporary.
					float sea_level_offset = 0.1f;
					if(value > (0.25f + sea_level_offset))
					{
						chosen_color = Colors.LightBlue;
					}
					if(value > (0.20f + sea_level_offset))
					{
						chosen_color = Colors.DimGray;
					}
					if(value > (0.15f + sea_level_offset))
					{
						chosen_color = Colors.DarkGreen;
					}
					else if(value > (0.05f + sea_level_offset))
					{
						chosen_color = Colors.Limegreen;
					}
					else if(value > (0f + sea_level_offset))
					{
						chosen_color = Colors.PaleGoldenrod;
					}
					else if(value > (-0.15f + sea_level_offset))
					{
						chosen_color = Colors.MediumBlue;
					}
					else
					{
						chosen_color = Colors.NavyBlue;
					}
					img.SetPixel(x, y, chosen_color);
				}
			}
			img.Unlock();
			return img;
		}
	}
}


