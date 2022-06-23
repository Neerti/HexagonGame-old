using System.Collections.Generic;
using Godot;
using Hexagon.Populations;

namespace Hexagon.Settlements
{
	/// <summary>
	/// JobSlots contain one or more Job objects, which can be filled by a Person object in order to have that person
	/// do things such as obtain resources, process resources, shape the map, find things, etc. The JobSlot object
	/// is held by a <see cref="Job"/> object, which manages a group of JobSlots.
	/// </summary>
	public class JobSlot : Node
	{
		public Job Holder;
		public JobSlotStatus Status = JobSlotStatus.Open;
		public bool Closed = false;
		public Person Worker;

		public JobSlot(Job source)
		{
			Holder = source;
		}
		
		[Signal]
		delegate void OnWorkerReplaced(Person oldWorker, Person newWorker);
	}
	

	public enum JobSlotStatus
	{
		/// <summary>
		/// The slot is available, and the game will try to fill it if possible.
		/// </summary>
		Open,
		
		/// <summary>
		/// The slot is already taken by someone.
		/// </summary>
		Filled,
		
		/// <summary>
		/// The slot cannot be occupied by anyone.
		/// </summary>
		Closed
	}
}