using JetBrains.Annotations;
using Godot;

namespace Hexagon.Settlements
{
	/// <summary>
	/// Buildings are structures that are linked to a Settlement, and provide a number of functions, such as
	/// storage space, or job slots. The functions are based on modules attached to the Building object.
	/// </summary>
	public class Building : Node
	{
		[CanBeNull]
		public Storage TryGetStorage()
		{
			return HasNode("Storage") ? GetNode<Storage>("Storage") : null;
		}

		public void Process()
		{
			
		}
	}
}