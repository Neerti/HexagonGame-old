using System;
using System.Collections.Generic;
using Xunit;
using Hexagon.Research.TechTrees;
using Hexagon.Research.TechTrees.ScienceTree;
using Hexagon.Research.TechTrees.TestTree;

namespace Hexagon.Tests
{
	public class TestTechnologyTreeTests
	{
		/// <summary>
		/// Tests GetAllParents() against a prebuilt test tree, which has a structure of;
		///			TestA	-	TestA2
		///			/	\
		/// TestRoot	TestAAndB
		///			\	/
		///			TestB
		/// </summary>
		[Fact]
		public void GetAllParents_ContainsRootNode_ShouldPassAllAsserts()
		{
			var testTechnologyTree = TechnologyTreeBuilder.MakeTree(typeof(TestNode));
			
			var results = testTechnologyTree.GetAllParentsOfNode(testTechnologyTree.GetNode(TechIDs.TestAAndB));
			
			// All of these nodes are ancestors of TestAAndB.
			Assert.Contains(testTechnologyTree.GetNode(TechIDs.TestRoot), results);
			Assert.Contains(testTechnologyTree.GetNode(TechIDs.TestA), results);
			Assert.Contains(testTechnologyTree.GetNode(TechIDs.TestB), results);
			
			// Node A2 is a child of Node A, so it should not be in the list.
			Assert.DoesNotContain(testTechnologyTree.GetNode(TechIDs.TestA2), results);
		}

		/// <summary>
		/// Tests all trees in the code for any nodes that cannot be reached from any root nodes.
		/// </summary>
		/// <param name="nodeType">Which node type to use for building the tree.</param>
		[Theory]
		[InlineData(typeof(ScienceNode))]
		[InlineData(typeof(TestNode))]
		public void TechnologyTrees_GetUnreachableNodes_ShouldBeEmpty(Type nodeType)
		{
			var tree = TechnologyTreeBuilder.MakeTree(nodeType);
			
			Assert.Empty(tree.GetUnreachableNodes());
		}

	}
}
