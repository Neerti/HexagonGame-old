using Godot;
using System;
using Hexagon.Globals;

namespace Hexagon.TitleScreen.About
{
	public class SourceCodeRepoButton : Button
	{
		public override void _Pressed()
		{
			OS.ShellOpen(GameInformation.SourceCodeURL);
		}
	}
}


