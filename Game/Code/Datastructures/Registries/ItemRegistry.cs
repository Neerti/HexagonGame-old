using System.Collections.Generic;
using Godot;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hexagon.Datastructures.Registries
{
	public class ItemRegistry
	{
		public Dictionary<string, ItemData> Registry;

//		public void Add(ItemData input)
//		{
//			Registry.Add(input.Kind, input);
//		}

		public ItemData Get(string kind)
		{
			return Registry[kind];
		}

		public ItemRegistry()
		{
			Populate();
		}

		public void Populate()
		{
			var file = new Godot.File();
			file.Open("res://Data/Items/items.json", Godot.File.ModeFlags.Read);
			
			var contractResolver = new DefaultContractResolver
			{
				NamingStrategy = new SnakeCaseNamingStrategy()
			};
			
			var result = JsonConvert.DeserializeObject<Dictionary<string, ItemData>>(file.GetAsText(), new JsonSerializerSettings
			{
				ContractResolver = contractResolver,
				Formatting = Formatting.Indented
			});
			Registry = result;
			
		}
	}
	
	

	public struct ItemData
	{

		public string DisplayName;

		public string Description;

		public int StackLimit;

		public string SpriteTexturePath;

		public string SpriteColor;

		public override string ToString()
		{
			return DisplayName;
		}
	}
}