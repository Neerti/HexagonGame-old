using Godot;
using System;

namespace Hexagon.TitleScreen.About
{
	public class CreativeCommons3BYButton : Button
	{
		public override void _Pressed()
		{
			OS.ShellOpen("https://creativecommons.org/licenses/by/3.0/");
		}
	}
}


