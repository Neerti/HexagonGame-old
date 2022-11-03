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
		/// <param name="itemKind">The ID of the item to look for.</param>
		/// <returns>Whether or not there is at least one item contained in storage.</returns>
		public bool HasItem(ItemStorageComponent component, string itemKind)
		{
			foreach (var itemInstance in component.Items)
			{
				if (itemKind == itemInstance.ItemKind)
				{
					return true;
				}
			}
			return false;
		}

		public List<ItemInstance> GetAllItemInstancesOfItemKind(ItemStorageComponent component, string itemKind)
		{
			var result = new List<ItemInstance>();
			foreach (var itemInstance in component.Items)
			{
				if (itemKind == itemInstance.ItemKind)
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
		/// <param name="itemKind">The ID of the item to consolidate.</param>
		public void ConsolidateItems(ItemStorageComponent component, string itemKind)
		{
			var matchingItemInstances = GetAllItemInstancesOfItemKind(component, itemKind);
			var totalAmount = 0;
			foreach (var itemInstance in matchingItemInstances)
			{
				totalAmount += itemInstance.Quantity;
				component.Items.Remove(itemInstance);
			}

			var newItemInstances = new List<ItemInstance>();

			var itemData = Singleton.ItemRegistry.Get(itemKind);
			
			while (totalAmount > 0)
			{
				var amount = Math.Min(itemData.StackLimit, totalAmount);
				newItemInstances.Add(new ItemInstance(itemKind, amount));
				totalAmount -= itemData.StackLimit;
			}
			
			component.Items.AddRange(newItemInstances);
			
		}

		
	}
}
