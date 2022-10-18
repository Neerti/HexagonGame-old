namespace Hexagon.Items
{
	public struct ItemInstance
	{
		public ItemIDs ItemID;
		public int Quantity;

		public ItemInstance(ItemIDs itemID, int quantity)
		{
			ItemID = itemID;
			Quantity = quantity;
		}
	}
}