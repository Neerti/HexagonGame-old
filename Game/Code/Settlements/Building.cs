using System.Collections.Generic;

namespace Hexagon.Settlements
{
	/// <summary>
	/// Buildings are structures that are linked to a Settlement, and provide a number of functions, such as
	/// storage space, or job slots. The functions are based on modules attached to the Building object.
	/// </summary>
	public class Building
	{
		public Storage Storage;

		public List<JobSlot> JobSlots;
	}
}