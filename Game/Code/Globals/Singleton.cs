using System.Collections.Generic;
using System.Linq;
using Hexagon.Items;
using Hexagon.Populations;
using Hexagon.Research.TechTrees;
using Hexagon.Research.TechTrees.ScienceTree;

namespace Hexagon.Globals
{
	public class Singleton
	{
		public static readonly TechnologyTree ScienceTechTree = TechnologyTreeBuilder.MakeTree(typeof(ScienceNode));

		public static readonly Dictionary<NeedKinds, Need> AllNeeds = PopulateNeeds();

		public static readonly Dictionary<ItemIDs, Item> AllItems = PopulateItems();

		// Private constructor, to prevent initialization of this class outside of static.
		private Singleton()
		{

		}

		private static Dictionary<NeedKinds, Need> PopulateNeeds()
		{
			var dict = new Dictionary<NeedKinds, Need>
			{
				{NeedKinds.Food, new FoodNeed()},
				{NeedKinds.Water, new WaterNeed()}
			};
			return dict;
		}

		private static Dictionary<ItemIDs, Item> PopulateItems()
		{
			var instanced_items = Helpers.InstancedSubtypesOf(typeof(Item)).Cast<Item>().ToList();
			var valid_items = new Dictionary<ItemIDs, Item>();
			foreach (var item in instanced_items)
			{
				if (item.ItemID is ItemIDs.Base)
				{
					continue;
				}
				valid_items.Add(item.ItemID, item);
			}

			return valid_items;
		}
		
		
	}
}