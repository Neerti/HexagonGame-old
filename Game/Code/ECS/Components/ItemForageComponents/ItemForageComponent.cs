using System;
using System.Collections.Generic;
using Hexagon.Items;

namespace Hexagon.ECS.Components.ItemForageComponents
{
    public class ItemForageComponent : Component
    {
        public List<ItemInstance> ItemPrototype;
    }

    public struct ItemForageInstance
    {
        public ItemInstance ItemInstance;

        public TimeSpan RenewalTime;
    }
    
}
