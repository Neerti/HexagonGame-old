using System.Collections.Generic;
using Hexagon.Items;

namespace Hexagon.ECS.Components
{
	public class ItemProductionComponent : Component
	{
		public List<ItemInstance> ItemPrototype;
	}

}