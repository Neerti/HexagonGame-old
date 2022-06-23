using Godot;

namespace Hexagon
{
	public class MapGenerator
	{
		OpenSimplexNoise height_noise;
		int default_octaves = 6;

		OpenSimplexNoise heat_noise;

		public MapGenerator(int new_seed)
		{
			height_noise = new OpenSimplexNoise();
			height_noise.Octaves = default_octaves;
			height_noise.Seed = new_seed;

			heat_noise = new OpenSimplexNoise();
			heat_noise.Octaves = default_octaves;
			heat_noise.Seed = new_seed+1;
		}

		public HexGrid GenerateNewMap(int new_x, int new_y)
		{
			var new_grid = new HexGrid(new_x, new_y);
			ApplyNoise(new_grid);
			new_grid.AssignBiomesToHexes();
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

					float y1 = (float)y;
					float x1 = (float)x / grid.SizeX;

					// Three dimensional coordinates, to sample the noise in the shape of a cylinder.
					// This allows for the map to wrap on the sides.
					float noise_x = Mathf.Sin(x1 * Mathf.Tau) / Mathf.Tau;
					float noise_y = Mathf.Cos(x1 * Mathf.Tau) / Mathf.Tau;
					float noise_z = y1;

					// [Cos/Sin]/Tau gives a range of [0, 1]. We scale this by grid.SizeX to get the range of the map.
					noise_x *= grid.SizeX;
					noise_y *= grid.SizeX;

					// Then divide by noise_sampling_scale to fine-tune the apparent map size.
					noise_x /= noise_sampling_scale;
					noise_y /= noise_sampling_scale;
					noise_z /= noise_sampling_scale;

					float height_value = height_noise.GetNoise3d(noise_x, noise_y, noise_z);

					Hex tile = grid.GetHex(x, y);
					tile.Height = height_value;


					// Temperature time.
					// This will equal 1 at the poles, and 0 at the equator.
					float polar_distance = Mathf.Abs(1.0f-(y / (grid.SizeY / 2.0f)));

					// Smooth out the transition from cold to hot.
				//	float equatorial_heat_band = Mathf.Cos(polar_distance * Mathf.Deg2Rad(90));
				//	tile.Temperature = Mathf.Lerp(0f, 1f, equatorial_heat_band);
					float equatorial_heat = Mathf.Lerp(1f, 0f, polar_distance);
					float heat_noise_value = heat_noise.GetNoise3d(noise_x, noise_y, noise_z) + 1.0f;

					float heat_value = heat_noise_value * equatorial_heat;
					
					if(tile.Height >= 0.25f)
					{
						heat_value -= (tile.Height - 0.25f) * 1.5f;
					}
					//heat_value -= (tile.Height * 1f);

					tile.Temperature = heat_value;

					// TODO: Height and random noise.
				}
			}
		}

	}
}
