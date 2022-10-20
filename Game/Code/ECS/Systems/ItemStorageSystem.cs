using System;
using System.Collections.Generic;
using Hexagon.ECS.Components;
using Hexagon.Globals;
using Hexagon.Items;

namespace Hexagon.ECS.Systems
{
	public class ItemStorageSystem : GameSystem
	{
		/// <summary>
		/// Tests if a specific item exists inside storage.
		/// </summary>
		/// <param name="component">The storage component to test.</param>
		/// <param name="itemID">The ID of the item to look for.</param>
		/// <returns>Whether or not there is at least one item contained in storage.</returns>
		public bool HasItem(ItemStorageComponent component, ItemIDs itemID)
		{
			foreach (var itemInstance in component.Items)
			{
				if (itemID == itemInstance.ItemID)
				{
					return true;
				}
			}
			return false;
		}

		public List<ItemInstance> GetAllItemInstancesOfItemID(ItemStorageComponent component, ItemIDs itemID)
		{
			var result = new List<ItemInstance>();
			foreach (var itemInstance in component.Items)
			{
				if (itemID == itemInstance.ItemID)
				{
					result.Add(itemInstance);
				}
			}

			return result;
		}
		
		
		/// <summary>
		/// Merges all <see cref="ItemInstance"/>s of a certain item together, using the minimum number of
		/// ItemInstances necessary.
		/// </summary>
		/// <param name="component">The storage component to act on.</param>
		/// <param name="itemID">The ID of the item to consolidate.</param>
		public void ConsolidateItems(ItemStorageComponent component, ItemIDs itemID)
		{
			var matchingItemInstances = GetAllItemInstancesOfItemID(component, itemID);
			var totalAmount = 0;
			foreach (var itemInstance in matchingItemInstances)
			{
				totalAmount += itemInstance.Quantity;
				component.Items.Remove(itemInstance);
			}

			var newItemInstances = new List<ItemInstance>();

			var itemData = Singleton.AllItems[itemID];
			
			while (totalAmount > 0)
			{
				var amount = Math.Min(itemData.StackSize, totalAmount);
				newItemInstances.Add(new ItemInstance(itemID, amount));
				totalAmount -= itemData.StackSize;
			}
			
			component.Items.AddRange(newItemInstances);
			
		}

		
	}
}
