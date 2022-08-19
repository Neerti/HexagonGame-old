using Godot;
using System;

namespace Hexagon.Map.MapDisplays
{
	public class VSyncButton : CheckButton
	{
		public override void _Toggled(bool buttonPressed)
		{
			OS.VsyncEnabled = buttonPressed;
		}
	}
}


