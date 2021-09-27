using Godot;

namespace Hexagon
{
	public class MapGenerator
	{
		OpenSimplexNoise height_noise;
		int default_octaves = 6;

		public MapGenerator(int new_seed)
		{
			height_noise = new OpenSimplexNoise();
			height_noise.Octaves = default_octaves;
			height_noise.Seed = new_seed;
		}

		public HexGrid GenerateNewMap(int new_x, int new_y)
		{
			var new_grid = new HexGrid(new_x, new_y);
			ApplyNoise(new_grid);
			return new_grid;
		}

		private void ApplyNoise(HexGrid grid)
		{
			var img = height_noise.GetImage(grid.SizeX, grid.SizeY);
			img.SavePng("res://test.png");

			for(int x = 0; x < grid.SizeX; x++)
			{
				for(int y = 0; y < grid.SizeY; y++)
				{
					// Sample the noise at smaller intervals.
					// Larger numbers make the map look bigger. Smaller ones have the opposite effect.
					float noise_sampling_scale = 3f;
					float x1 = x / noise_sampling_scale;
					float y1 = y / noise_sampling_scale;

					// Applies height to the tile. Higher equals higher up.
					float value = height_noise.GetNoise2d(x1, y1);

					Hex tile = grid.GetHex(x, y);
					tile.Height = value;

					/*
					//Noise range
					float x1 = 0, x2 = 1;
					float y1 = 0, y2 = 1;               
					float dx = x2 - x1;
					float dy = y2 - y1;

					//Sample noise at smaller intervals
					float s = x / (float)Width;
					float t = y / (float)Height;

					// Calculate our 3D coordinates
					float nx = x1 + Mathf.Cos (s * 2 * Mathf.PI) * dx / (2 * Mathf.PI);
					float ny = x1 + Mathf.Sin (s * 2 * Mathf.PI) * dx / (2 * Mathf.PI);
					float nz = t;

					float heightValue = (float)HeightMap.Get (nx, ny, nz);

					// keep track of the max and min values found
					if (heightValue > mapData.Max)
						mapData.Max = heightValue;
					if (heightValue < mapData.Min)
						mapData.Min = heightValue;

					mapData.Data [x, y] = heightValue;
					*/
				}
			}
		}

/*
        // loop through each x,y point - get height value
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                //Sample the noise at smaller intervals
                float x1 = x / (float)Width;
                float y1 = y / (float)Height;
 
                float value = (float)HeightMap.Get (x1, y1);
 
                //keep track of the max and min values found
                if (value > mapData.Max) mapData.Max = value;
                if (value < mapData.Min) mapData.Min = value;
 
                mapData.Data[x,y] = value;
            }
        }   
*/

	}
}
