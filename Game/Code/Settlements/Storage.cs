using System;
using Godot;
using Godot.Collections;
using Hexagon.Items;

namespace Hexagon.Settlements
{
	public class Storage : Node
	{
		public Dictionary<ItemIDs, int> Items = new Dictionary<ItemIDs, int>();

		public override void _Ready()
		{
			StarterItems(); // TODO remove after testing
		}

		public void StarterItems()
		{
			AddItem(ItemIDs.CleanWater, 10);
			AddItem(ItemIDs.Berry, 20);
		}

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

		public bool HasItem(ItemIDs thing, int amount = 1)
		{
			if (Items.ContainsKey(thing))
			{
				return amount <= Items[thing];
			}

			return false;
		}

		public void RemoveItem(ItemIDs thing, int amount)
		{
			if (!HasItem(thing))
			{
				throw new Exception("Was asked to remove an item (" + thing + ") that it doesn't have.");
			}

			if (!HasItem(thing, amount))
			{
				throw new Exception("Was asked to remove more items (" + thing +") than it has.");
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


