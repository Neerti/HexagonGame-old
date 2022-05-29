using Godot;
using System;

namespace Hexagon.TitleScreen
{
	public class AboutButton : Button
	{
		public override void _Pressed()
		{
			var aboutWindow = GetNode<Control>("/root/TitleScreen/About");
			aboutWindow.Visible = true;
		}
	}
}


