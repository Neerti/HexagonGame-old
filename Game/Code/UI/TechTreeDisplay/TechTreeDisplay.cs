using Godot;

namespace Hexagon.UI.TechTreeDisplay
{
	public class TechTreeDisplay : Control
	{
		public override void _Input(InputEvent @event)
		{
			if (@event.IsActionPressed("toggle_technology_ui"))
			{
				Visible = !Visible;
			}
		}
	}
}


