
using System.Collections.Generic;
using Godot;
using Hexagon.Globals;
using Hexagon.Populations;

namespace Hexagon.Settlements
{
	public static class NeedManager
	{
		public static void ProcessNeeds(Settlement settlement)
		{
			var relevantNeeds = new Dictionary<NeedKinds, List<NeedTicket>>();
			var needSums = new Dictionary<NeedKinds, float>();
			
			// Population loop.
			foreach (var person in settlement.GetPopulation())
			{
				// Each person is asked what they need and how much.
				var ticket = person.GetNeedTicket();
				foreach (var pair in ticket.Request)
				{
					if (!relevantNeeds.ContainsKey(pair.Key))
					{
						relevantNeeds[pair.Key] = new List<NeedTicket>();
					}
					relevantNeeds[pair.Key].Add(ticket);

					if (!needSums.ContainsKey(pair.Key))
					{
						needSums[pair.Key] = 0f;
					}

					needSums[pair.Key] += pair.Value;
				}
			}
			
			// Storage loop.
			var storageSums = new Dictionary<NeedKinds, float>();
			foreach (var needKind in Singleton.AllNeeds.Keys)
			{
				storageSums.Add(needKind, 0f);
			}
			
			// Look in all the storage objects in the settlement.
			foreach (var storage in settlement.GetAllStorage())
			{
				// Check each item inside.
				foreach (var itemQuantityPair in storage.Items)
				{
					var actualItem = Singleton.AllItems[itemQuantityPair.Key];
					if (actualItem.NeedFulfill.Count == 0)
					{
						continue;
					}
					
					foreach (var needFulfillmentPair in actualItem.NeedFulfill)
					{
						storageSums[needFulfillmentPair.Key] += needFulfillmentPair.Value * itemQuantityPair.Value;
					}
					
				}
			}
			
			// Distribution loop.
			foreach (var currentNeed in Singleton.AllNeeds.Keys)
			{
				var relevant = relevantNeeds[currentNeed];
				var demand = needSums[currentNeed];
				var available = storageSums[currentNeed];

				// If there isn't enough supplies available to meet demand, everyone will get less.
				// It's better if everyone gets half of a meal verses half getting a full meal
				// and the other half getting nothing.
				var distro_mut = 1f;
				if (available < demand)
				{
					distro_mut = available / demand;
				}
				
				GD.Print("Demand: " + demand);
				GD.Print("Available: " + available);

				foreach (var ticket in relevant)
				{
					// Gives the food/water/whatever out to the person. It might be less than what they want or need, 
					// but that isn't our problem.
					ticket.Sender.FulfillNeed(currentNeed, ticket.Request[currentNeed] * distro_mut);
				}
			}
		}
		
		/*
		let mut relevant_needs: BTreeMap<NeedKinds, Vec<Need>> = BTreeMap::new();
		let mut need_sums: BTreeMap<NeedKinds, u32> = BTreeMap::new();

		for ticket in settlement {
		    for need in ticket.all_needs() {
		        relevant_needs
		            .entry(need.kind)
		            .or_insert_with(Default::default)
		            .push(&need);
		        let mut need_sum = need_sums
		            .entry(need.kind)
		            .or_insert_with(Default::default)
		        need_sum += need.amount;
		    }
		}

		let mut storage_sums: BTreeMap<NeedKinds, u32> = BTreeMap::new();

		for item in storage {
		    if !item.suitable() {
		        continue;
		    }
		    for (need_kind, amount) in item.suitable_needs {
		        let mut sum = storage_sums
		            .entry(need_kind)
		            .or_insert_with(Default::default);
		        sum += amount
		    }
		}

		for need_kind in need_kinds {
		    let relevant = &relevant_needs[need_kind];
		    let need = &need_sums[need_kind];
		    let available = &storage_sums[need_kind];

		    let mut distro_mut = 1;
		    if available < need {
		        distro_mut = available / need;
		    }

		    for ticket in relevant {
		        ticket.fulfill_need(need_kind, need * distro_mut);
		    }
		}
		*/
	}
}