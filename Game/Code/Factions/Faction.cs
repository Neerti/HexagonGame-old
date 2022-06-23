using Godot;
using Hexagon.Settlements;

namespace Hexagon.Factions
{
	
	/// <summary>
	/// A faction, in the game, represents a specific player and all of their holdings.
	/// </summary>
	public class Faction : Node
	{
		public void ProcessSettlements()
		{
			GD.Print(Name + " was processed.");
			var settlements = GetNode<Node>("Settlements");
			foreach (var child in settlements.GetChildren())
			{
				var settlement = (Settlement) child;
				settlement.ProcessSettlement();
			}
		}
	}
}

