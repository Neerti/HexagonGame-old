namespace Hexagon.Research.TechTrees.ScienceTree
{
	/// <summary>
	/// Base class for nodes that are part of the science tree.
	/// </summary>
	public class ScienceNode : TechnologyNode { }

	public class Speech : ScienceNode
	{
		public Speech()
		{
			TechID = TechIDs.Speech;
			DisplayName = "Speech";
			RootNode = true;
		}
	}
	
	public class Language : ScienceNode
	{
		public Language()
		{
			TechID = TechIDs.Language;
			DisplayName = "Language";
			ParentTechIDs.Add(TechIDs.Speech );
		}
	}
	
	public class Writing : ScienceNode
	{
		public Writing()
		{
			TechID = TechIDs.Writing;
			DisplayName = "Writing";
			ParentTechIDs.Add(TechIDs.Language );
		}
	}
	
	public class Foraging : ScienceNode
	{
		public Foraging()
		{
			TechID = TechIDs.Foraging;
			DisplayName = "Foraging";
			RootNode = true;
		}
	}
	
	public class StoneKnapping : ScienceNode
	{
		public StoneKnapping()
		{
			TechID = TechIDs.StoneKnapping;
			DisplayName = "Stone Knapping";
			ShortDescription = "The first tools.";
			Description = "Knapping, or Lithic reduction, is a process that sharpens a stone by " +
			              "removing parts of it, using blunt force.";
			GameplayEffects = "Enables crafting of sharpened stones.";
			ParentTechIDs.Add(TechIDs.Foraging );
		}
	}
	
	public class BoneWorking : ScienceNode
	{
		public BoneWorking()
		{
			TechID = TechIDs.BoneWorking;
			DisplayName = "Bone Working";
			Description = "Bone is a fairly hard material that can occasionally be found while looking for food, " +
			              "whether from scavenging cadavers, or hunting animals. Unlike stone or wood, it comes in " +
			              "a number of shapes that might be desirable.";
			ParentTechIDs.Add(TechIDs.StoneKnapping );
		}
	}
	
	public class Sewing : ScienceNode
	{
		public Sewing()
		{
			TechID = TechIDs.Sewing;
			DisplayName = "Sewing";
			ParentTechIDs.Add(TechIDs.BoneWorking );
		}
	}
	
	public class LeatherWorking : ScienceNode
	{
		public LeatherWorking()
		{
			TechID = TechIDs.LeatherWorking;
			DisplayName = "Leather Working";
			ParentTechIDs.Add(TechIDs.Sewing );
		}
	}
	
	public class Woodcutting : ScienceNode
	{
		public Woodcutting()
		{
			TechID = TechIDs.Woodcutting;
			DisplayName = "Woodcutting";
			ShortDescription = "Easier access to wood.";
			Description = "Sharp stones held in hand makes it possible to fell trees with thin trunks. " +
			              "This allows for the deliberate harvesting of wood, without needing to forage for " +
			              "twigs, or hope to find a dead tree that's already on the ground.";
			GameplayEffects = "Allows for cutting down small trees, improving access to wooden materials.";
			ParentTechIDs.Add(TechIDs.StoneKnapping );
		}
	}
	
	public class ToolMaking : ScienceNode
	{
		public ToolMaking()
		{
			TechID = TechIDs.ToolMaking;
			DisplayName = "Tool Making";
			ShortDescription = "The second tools.";
			Description = "With access to stronger pieces of wood, sharpened stones can be affixed to wood, acting " +
			              "as handles. This allows for more force to be applied, and less of a chance of cutting " +
			              "the user's hand from squeezing too hard.";
			GameplayEffects = "Enables crafting of tools, that certain roles may require in the future.";
			ParentTechIDs.Add(TechIDs.Woodcutting );
		}
	}
	
	public class Digging : ScienceNode
	{
		public Digging()
		{
			TechID = TechIDs.Digging;
			DisplayName = "Digging";
			Description = "Excavation can be improved greatly with a specialized tool, one that possesses a wide " +
			              "head to move earth faster and more efficiently than hand digging. " +
			              "A specific bone from a large animal might be suitable as the tool head.";
			GameplayEffects = "Allows crafting of bone shovels, and also enables the Digger role, " +
			                  "allowing for targeted collection of materials such as dirt, sand, or clay.";
			ParentTechIDs.Add(TechIDs.ToolMaking);
			ParentTechIDs.Add(TechIDs.BoneWorking);
		}
	}
	
	public class Mining : ScienceNode
	{
		public Mining()
		{
			TechID = TechIDs.Mining;
			DisplayName = "Mining";
			ShortDescription = "Strike the earth.";
			Description = "New kinds of materials might be found inside of rock, however a new kind of tool is " +
			              "needed to extract it. A tool that concentrates force into a small point would " +
			              "aid in shattering large rocks into smaller rocks. The rocks can then be collected " +
			              "or discarded.";
			GameplayEffects = "Allows for crafting of stone pickaxes, and also enables the Miner role, " +
			                  "allowing for targeted collection of stones and surface veins.";
			ParentTechIDs.Add(TechIDs.Digging);
		}
	}
	
	public class Burial : ScienceNode
	{
		public Burial()
		{
			TechID = TechIDs.Burial;
			DisplayName = "Burial";
			ShortDescription = "Rest In Peace";
			Description = "It is a fact of life that eventually life ends. It's not a good idea to leave corpses " +
			              "out in the open to decay. Burying them both gets them out of the way, and may make " +
			              "those who still live feel a little better.";
			GameplayEffects = "Can dig graves, which provides a dignified resting place for the deceased.";
			ParentTechIDs.Add(TechIDs.Digging);
		}
	}
	
	public class Prospecting : ScienceNode
	{
		public Prospecting()
		{
			TechID = TechIDs.Prospecting;
			DisplayName = "Prospecting";
			ShortDescription = "Know where to dig.";
			ParentTechIDs.Add(TechIDs.Mining);
		}
	}
	
	public class Quarrying : ScienceNode
	{
		public Quarrying()
		{
			TechID = TechIDs.Quarrying;
			DisplayName = "Quarrying";
			ShortDescription = "Rock and stone.";
			ParentTechIDs.Add(TechIDs.Mining);
		}
	}
	
	public class Stonemasonry : ScienceNode
	{
		public Stonemasonry()
		{
			TechID = TechIDs.Stonemasonry;
			DisplayName = "Stonemasonry";
			ShortDescription = "Durable structures and sculptures made from cut stone.";
			ParentTechIDs.Add(TechIDs.Quarrying);
		}
	}
	
	public class Building : ScienceNode
	{
		public Building()
		{
			TechID = TechIDs.Building;
			DisplayName = "Building";
			ShortDescription = "Long-term structures.";
			ParentTechIDs.Add(TechIDs.ToolMaking );
		}
	}
	
	public class Carpentry : ScienceNode
	{
		public Carpentry()
		{
			TechID = TechIDs.Carpentry;
			DisplayName = "Carpentry";
			ShortDescription = "Shaping wood into more useful forms.";
			ParentTechIDs.Add(TechIDs.Building );
		}
	}
	
	public class Construction : ScienceNode
	{
		public Construction()
		{
			TechID = TechIDs.Construction;
			DisplayName = "Construction";
			ShortDescription = "Better buildings from better materials.";
			ParentTechIDs.Add(TechIDs.Carpentry );
			ParentTechIDs.Add(TechIDs.Stonemasonry );
		}
	}

	public class Carving : ScienceNode
	{
		public Carving()
		{
			TechID = TechIDs.Carving;
			DisplayName = "Carving";
			ShortDescription = "Ancient art.";
			ParentTechIDs.Add(TechIDs.StoneKnapping );
		}
	}
	
	public class GemCutting : ScienceNode
	{
		public GemCutting()
		{
			TechID = TechIDs.GemCutting;
			DisplayName = "Gem Cutting";
			ShortDescription = "Shiny and pretty.";
			ParentTechIDs.Add(TechIDs.Carving );
		}
	}
	

	
	public class WellDigging : ScienceNode
	{
		public WellDigging()
		{
			TechID = TechIDs.WellDigging;
			DisplayName = "Well Digging";
			ShortDescription = "A new source of water.";
			ParentTechIDs.Add(TechIDs.Digging );
			ParentTechIDs.Add(TechIDs.Building );
		}
	}
	
	public class FireMaking : ScienceNode
	{
		public FireMaking()
		{
			TechID = TechIDs.FireMaking;
			DisplayName = "Fire Making";
			Description = "Certain materials, such as wood, are combustible. Nature sometimes causes these materials " +
			              "to burn, but they could also be ignited deliberately. The control of fire would " +
			              "provide numerous benefits such as warmth, lighting, warding predators, and the ability to " +
			              "heat up objects to change them.";
			ParentTechIDs.Add(TechIDs.Foraging);
		}
	}
	
	public class Cooking : ScienceNode
	{
		public Cooking()
		{
			TechID = TechIDs.Cooking;
			DisplayName = "Cooking";
			ParentTechIDs.Add(TechIDs.FireMaking);
			GameplayEffects = "Allows for preparing food, improving nutritional value.";
		}
	}
	
	public class Ceramics : ScienceNode
	{
		public Ceramics()
		{
			TechID = TechIDs.Ceramics;
			DisplayName = "Ceramics";
			GameplayEffects = "Can craft ceramic pots, which act as containers for various goods. " +
			                  "Containers help preserve their contents, and enables storing certain " +
			                  "resources that might otherwise not be possible to hold normally, such " +
			                  "as water.";
			ParentTechIDs.Add(TechIDs.Cooking);
		}
	}
	
	public class Stockpiling : ScienceNode
	{
		public Stockpiling()
		{
			TechID = TechIDs.Stockpiling;
			DisplayName = "Stockpiling";
			ShortDescription = "A reserve of goods can provide some much needed stability.";
			GameplayEffects = "Allows for building stockpiles, which act as the first storage facility.";
			ParentTechIDs.Add(TechIDs.Ceramics);
		}
	}
	
	public class Smelting : ScienceNode
	{
		public Smelting()
		{
			TechID = TechIDs.Smelting;
			DisplayName = "Smelting";
			ParentTechIDs.Add(TechIDs.Cooking);
			ParentTechIDs.Add(TechIDs.Mining);
		}
	}
	
	public class GoldWorking : ScienceNode
	{
		public GoldWorking()
		{
			TechID = TechIDs.GoldWorking;
			DisplayName = "Gold Working";
			ShortDescription = "Soft, but shiny.";
			ParentTechIDs.Add(TechIDs.Smelting);
		}
	}
	
	public class Monuments : ScienceNode
	{
		public Monuments()
		{
			TechID = TechIDs.Monuments;
			DisplayName = "Monuments";
			ShortDescription = "Awe-inspiring structures to commemorate something extraordinary.";
			ParentTechIDs.Add(TechIDs.Construction );
			ParentTechIDs.Add(TechIDs.GemCutting );
			ParentTechIDs.Add(TechIDs.GoldWorking );
		}
	}
}