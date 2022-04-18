using System;
using Hexagon.Items;
using Hexagon.Settlements;
using Xunit;

namespace Hexagon.Tests
{
	public class StorageTests
	{
		private readonly Storage _storage;
		public StorageTests()
		{
			_storage = new Storage();
		}

		
		// AddItem()
		[Fact]
		public void AddItem_AddingSingleItem_ShouldAddSuccessfully()
		{
			_storage.AddItem(ItemIDs.Stick, 1);
			
			Assert.Single(_storage.Items);
		}
		
		[Fact]
		public void AddItem_AddingTwoIdenticalItems_ShouldHaveOneKey()
		{
			_storage.AddItem(ItemIDs.Stick, 1);
			_storage.AddItem(ItemIDs.Stick, 1);
			
			Assert.Single(_storage.Items);
		}
		
		[Fact]
		public void AddItem_AddingTwoDistinctItems_ShouldHaveTwoKeys()
		{
			_storage.AddItem(ItemIDs.Stick, 1);
			_storage.AddItem(ItemIDs.Stone, 1);
			
			Assert.Equal(2, _storage.Items.Count);
		}

		
		// HasItem()
		[Fact]
		public void HasItem_EmptyStorage_ShouldReturnFalse()
		{
			Assert.False(_storage.HasItem(ItemIDs.Apple));
		}
		
		[Fact]
		public void HasItem_ItemInStorage_ShouldReturnTrue()
		{
			_storage.AddItem(ItemIDs.Apple, 1);
			
			Assert.True(_storage.HasItem(ItemIDs.Apple));
		}
		
		[Fact]
		public void HasItem_DifferentItemInStorage_ShouldReturnFalse()
		{
			_storage.AddItem(ItemIDs.Stick, 1);
			
			Assert.False(_storage.HasItem(ItemIDs.Apple));
		}

		[Fact]
		public void HasItem_NotEnoughItemsInStorage_ShouldReturnFalse()
		{
			_storage.AddItem(ItemIDs.Apple, 1);

			Assert.False(_storage.HasItem(ItemIDs.Apple, 2));
		}
		
		[Fact]
		public void HasItem_SomeItemsInStorage_ShouldReturnTrue()
		{
			_storage.AddItem(ItemIDs.Apple, 2);

			Assert.True(_storage.HasItem(ItemIDs.Apple, 2));
		}

		
		// RemoveItem()
		[Fact]
		public void RemoveItem_DoesHaveOne_ShouldRemoveKey()
		{
			_storage.AddItem(ItemIDs.Apple, 1);
			_storage.RemoveItem(ItemIDs.Apple, 1);
			
			Assert.Empty(_storage.Items);
		}
		
		[Fact]
		public void RemoveItem_DoesHaveMore_ShouldNotRemoveKey()
		{
			_storage.AddItem(ItemIDs.Apple, 2);
			_storage.RemoveItem(ItemIDs.Apple, 1);
			
			Assert.Single(_storage.Items);
		}
		
		[Fact]
		public void RemoveItem_DoesNotHave_ShouldThrowException()
		{
			void Code()
			{
				_storage.RemoveItem(ItemIDs.Apple, 1);
			}
			var ex = Assert.Throws<Exception>((Action) Code);
			
			Assert.NotNull(ex);
		}
		
		[Fact]
		public void RemoveItem_DoesNotHaveEnough_ShouldThrowException()
		{
			void Code()
			{
				_storage.AddItem(ItemIDs.Apple, 1);
				_storage.RemoveItem(ItemIDs.Apple, 2);
			}
			var ex = Assert.Throws<Exception>((Action) Code);
			
			Assert.NotNull(ex);
		}
	}
}