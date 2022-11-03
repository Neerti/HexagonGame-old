using System.Collections.Generic;
using System.Linq;
using Hexagon.Datastructures.Registries;
using Hexagon.Items;
using Hexagon.Map.Logical.Terrains;
using Hexagon.Populations;
using Hexagon.Research.TechTrees;
using Hexagon.Research.TechTrees.ScienceTree;

namespace Hexagon.Globals
{
	public class Singleton
	{
		public static readonly ItemRegistry ItemRegistry = new ItemRegistry();
		
		public static readonly TechnologyTree ScienceTechTree = TechnologyTreeBuilder.MakeTree(typeof(ScienceNode));

		public static readonly Dictionary<NeedKinds, Need> AllNeeds = PopulateNeeds();

		public static readonly Dictionary<ItemIDs, Item> AllItems = PopulateItems();

		public static readonly Dictionary<TerrainKinds, Terrain> AllTerrains = PopulateTerrains();

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
		
		private static Dictionary<TerrainKinds, Terrain> PopulateTerrains()
		{
			var instanced_terrains = Helpers.InstancedSubtypesOf(typeof(Terrain)).Cast<Terrain>().ToList();
			var valid_terrains = new Dictionary<TerrainKinds, Terrain>();
			foreach (var terrain in instanced_terrains)
			{
				if (terrain.TerrainKind is TerrainKinds.Base)
				{
					continue;
				}
				valid_terrains.Add(terrain.TerrainKind, terrain);
			}

			return valid_terrains;
		}
		
	}
}