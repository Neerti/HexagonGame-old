using Godot;

namespace Hexagon
{
	public class MapDisplay : Node2D
	{
		public override void _Ready()
		{
			var MapGen = new MapGenerator(2);
			var grid = MapGen.GenerateNewMap(1024, 1024);
			var tileMap = GetNode<TileMap>("TileMap");

			for (int x = 0; x < grid.SizeX; x++)
			{
				for (int y = 0; y < grid.SizeY; y++)
				{
					tileMap.SetCell(x, y, 1);
					var hex = tileMap.GetCell(x, y);
					
				}
			}
		}
	}
}

