using System.Collections.Generic;
using Hexagon.Items;

namespace Hexagon.ECS.Components
{
	public class ItemStorageComponent : Component
	{
		public List<ItemInstance> Items;
	}

}

