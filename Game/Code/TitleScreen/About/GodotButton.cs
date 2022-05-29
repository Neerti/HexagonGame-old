using Godot;
using System;

namespace Hexagon.TitleScreen.About
{
	public class GodotButton : Button
	{
		public override void _Pressed()
		{
			OS.ShellOpen("https://godotengine.org/");
		}
	}
}


