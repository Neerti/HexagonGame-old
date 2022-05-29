using Godot;
using System;

namespace Hexagon.TitleScreen.About
{
	public class GameIconsDotNetButton : Button
	{
		public override void _Pressed()
		{
			OS.ShellOpen("https://game-icons.net/");
		}
	}
}


