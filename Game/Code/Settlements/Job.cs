using System.Collections.Generic;
using Godot;
using Hexagon.Populations;

namespace Hexagon.Settlements
{
	/// <summary>
	/// A Job object is responsible for managing <see cref="JobSlot"/>s associated
	/// with a particular <see cref="Building"/>.
	/// </summary>
	public class Job : Node
	{
		/// <summary>
		/// The <see cref="JobSlot"/>s that this object manages. Each slot can be filled by
		/// a <see cref="Person"/>, if someone is available and qualified.
		/// </summary>
		public List<JobSlot> Slots = new List<JobSlot>();
		
		/// <summary>
		/// How many <see cref="JobSlot"/>s can exist at one time.
		/// </summary>
		public int MaxSlots = 5;

		public void MakeJobSlots()
		{
			for (int i = 0; i < MaxSlots; i++)
			{
				var newSlot = new JobSlot(this);
				Slots.Add(newSlot);
			}
		}
		
		
	}
}