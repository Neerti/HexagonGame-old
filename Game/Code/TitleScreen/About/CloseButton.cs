using Godot;
using System;

namespace Hexagon.TitleScreen.About
{
	public class CloseButton : Button
	{
		public override void _Pressed()
		{
			var box = GetNode<PanelContainer>("../..");
			box.Visible = false;
		}
	}
}


