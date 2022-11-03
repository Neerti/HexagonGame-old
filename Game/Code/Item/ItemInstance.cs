namespace Hexagon.Items
{
	public struct ItemInstance
	{
		public string ItemKind;
		public int Quantity;

		public ItemInstance(string itemKind, int quantity)
		{
			ItemKind = itemKind;
			Quantity = quantity;
		}
	}
}