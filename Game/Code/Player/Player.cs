using System.Collections.Generic;
using Hexagon.Settlements;

namespace Hexagon.Players
{
	/// <summary>
	/// The Player object holds information specific to either the player themselves or one of the AI factions.
	/// If multiplayer ever happens, they'll have other player objects too.
	/// </summary>
	public class Player
	{
		public List<Settlement> Settlements;
	}
}