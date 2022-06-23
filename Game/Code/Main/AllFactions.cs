using Godot;
using Hexagon.Factions;

namespace Hexagon.Main
{
	/// <summary>
	/// Groups all factions in the game under this Godot node. All children are factions.
	/// </summary>
	public class AllFactions : Node
	{

		public void ProcessFactions()
		{
			foreach (var child in GetChildren())
			{
				var faction = (Faction) child;
				faction.ProcessSettlements();
			}
		}
	}
}


