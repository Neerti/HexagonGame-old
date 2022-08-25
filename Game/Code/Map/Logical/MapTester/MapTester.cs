using Godot;
using System;
namespace Hexagon
{
	public class MapTester : Node
	{

		public override void _Ready()
		{
			var MapGen = new MapGenerator(2);
			var grid = MapGen.GenerateNewMap(1024, 1024);
		}
		

	}
}

