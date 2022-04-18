using System;
using Godot;
using Godot.Collections;
using Hexagon.Items;

namespace Hexagon.Settlements
{
	public class Storage : Node
	{
		public Dictionary<ItemIDs, int> Items = new Dictionary<ItemIDs, int>();

		public bool CanStoreItem(ItemIDs thing, int amount)
		{
			// TODO implement storage limits.
			return true;
		}
		
		public void AddItem(ItemIDs thing, int amount)
		{
			if (HasItem(thing))
			{
				// Already has the item, add more to the count.
				Items[thing] += amount;
			}
			else
			{
				Items.Add(thing, amount);
			}
		}

		public bool HasItem(ItemIDs thing)
		{
			return Items.ContainsKey(thing);
		}

		public void RemoveItem(ItemIDs thing, int amount)
		{
			if (!HasItem(thing))
			{
				throw new Exception("Was asked to remove an item that it doesn't have.");
			}

			if (Items[thing] < amount)
			{
				throw new Exception("Was asked to remove more items than it has.");
			}

			Items[thing] -= amount;
			// If no items are left, remove the key entirely.
			if (Items[thing] == 0)
			{
				Items.Remove(thing);
			}
		}
	} 
}


