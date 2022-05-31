using Godot;

namespace Hexagon.UI.TechTreeDisplay
{
	public class TechUIContent : HBoxContainer
	{
		public override void _Ready()
		{
			var treeUI = GetNode<TechUITree>("TechUITree");
			var techInfo = GetNode<TechnologyInfoRows>("PanelContainer/ScrollContainer/TechnologyInfoRows");

			foreach (var thing in treeUI.GetChildren())
			{
				GD.Print(thing);
				if (thing is TechUINode techNode)
				{
					GD.Print(techNode.GetType());
					//techInfo.Connect(nameof(TechUINode.UINodeClicked), techNode, nameof(techInfo.DescribeTechnology));
					//techInfo.Connect(nameof(techInfo.DescribeTechnology), techNode, nameof(TechUINode.UINodeClicked));
					
					// emitter.Connect ("emitter_signal_name", receiver, "receiver_callback_name")

					techNode.Connect(nameof(TechUINode.UINodeClicked), techInfo, nameof(techInfo.DescribeTechnology));
				}
			}
			
			
		}
	}
}


