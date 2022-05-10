using System.Collections.Generic;
using Xunit;
using Hexagon.Research.TechTrees;

namespace Hexagon.Tests
{
	public class TechnologyTreeTests
	{
		private readonly TechnologyTree _techTree;
		public TechnologyTreeTests()
		{
			_techTree = TechnologyTreeBuilder.MakeTree(typeof(TestNode));
		}

		[Fact]
		public void TechnologyTreeBuilder_MakeTestTree_ShouldMakeAllNodes()
		{
			// TODO: Make this not a magic number.
			Assert.Equal(5, _techTree.Nodes.Count);
		}

		[Fact]
		public void GetAllParents_ContainsRootNode_ShouldReturnTrue()
		{
			var results = _techTree.GetAllParentsOfNode(_techTree.GetNode(TechIDs.TestAAndB));
			
			// All of these nodes are ancestors of TestAAndB.
			Assert.Contains(_techTree.GetNode(TechIDs.TestRoot), results);
			Assert.Contains(_techTree.GetNode(TechIDs.TestA), results);
			Assert.Contains(_techTree.GetNode(TechIDs.TestB), results);
			
			// Node A2 is a child of Node A.
			Assert.DoesNotContain(_techTree.GetNode(TechIDs.TestA2), results);
		}

		[Fact]
		public void TestTree_NoUnreachableNodes_ShouldHaveEmptyList()
		{
			var unreachableNodes = new List<TechnologyNode>();
			foreach (var pair in _techTree.Nodes)
			{
				if (_techTree.IsNodeUnreachable(pair.Value))
				{
					unreachableNodes.Add(pair.Value);
				}
			}
			
			Assert.Empty(unreachableNodes);
		}

	}
}
