using Godot;
using System;

namespace Hexagon.TitleScreen
{
	public class QuitButton : Button
	{
		public override void _Pressed()
		{
			GetTree().Quit();
		}
	}
}


