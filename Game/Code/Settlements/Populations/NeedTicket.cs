using System.Collections.Generic;

namespace Hexagon.Populations
{
	/// <summary>
	/// NeedTickets hold information about a specific <see cref="Person"/>'s request for fulfilling needs.
	/// The tickets are sent up to the settlement, which handles a flood of tickets every in-game day.
	/// </summary>
	public readonly struct NeedTicket
	{
		public Person Sender { get; }
		public Dictionary<NeedKinds, float> Request { get; }

		public NeedTicket(Person sender, Dictionary<NeedKinds, float> request)
		{
			Sender = sender;
			Request = request;
		}

		public override string ToString()
		{
			var result = Sender.ToString();
			foreach (var pair in Request)
			{
				result += " (" + pair.Key + ":" + pair.Value + ")";
			}
			return result;
		}
	}
}