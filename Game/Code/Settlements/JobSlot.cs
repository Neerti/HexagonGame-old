using System.Collections.Generic;
using Hexagon.Populations;

namespace Hexagon.Settlements
{
	/// <summary>
	/// JobSlots contain one or more Job objects, which can be filled by a Person object in order to have that person
	/// do things such as obtain resources, process resources, shape the map, find things, etc. The JobSlot object
	/// is held by Buildings.
	/// </summary>
	public class JobSlot
	{
		public Job Job;
		public int TotalSlots = 1;
		public List<Person> SlotsFilled;
	}
}