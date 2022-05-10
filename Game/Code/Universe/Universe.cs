using System.Collections.Generic;
using Hexagon.Players;

namespace Hexagon.Universes
{
	/// <summary>
	/// The Universe object holds mutable state about a particular game. It persists across multiple sessions,
	/// but is not shared between separate save files. 
	/// Note that this object is not directly a global object, but is instead held by a LoadedUniverse object,
	/// which is static.
	/// </summary>
	public class Universe
	{
		public List<Player> Players;
	}
}