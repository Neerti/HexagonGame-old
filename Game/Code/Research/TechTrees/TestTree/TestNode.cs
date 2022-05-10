using System.Collections.Generic;

namespace Hexagon.Research.TechTrees.TestTree
{
	/// <summary>
	/// Generic testing node. Subtypes of this will be automatically 
	/// created in relevant unit tests.
	/// </summary>
	public class TestNode : TechnologyNode { }
	
	/// <summary>
	/// Node that is the root of the tech tree.
	/// </summary>
	public class NodeRoot : TestNode
	{
		public NodeRoot()
		{
			TechID = TechIDs.TestRoot;
			DisplayName = "I Am Root";
			RootNode = true; // So other unit tests don't think this tree is unreachable.
		}
	}
	
	/// <summary>
	/// Node with root as it's parent.
	/// </summary>
	public class NodeA : TestNode
	{
		public NodeA()
		{
			TechID = TechIDs.TestA;
			DisplayName = "A";
			ParentTechIDs.Add( TechIDs.TestRoot );
		}
	}
	
	/// <summary>
	/// Node with root as it's parent.
	/// </summary>
	public class NodeB : TestNode
	{
		public NodeB()
		{
			TechID = TechIDs.TestB;
			DisplayName = "B";
			ParentTechIDs.Add( TechIDs.TestRoot );
		}
	}
	
	/// <summary>
	/// Node with both the A and B nodes as parents.
	/// </summary>
	public class NodeAAndB : TestNode
	{
		public NodeAAndB()
		{
			TechID = TechIDs.TestAAndB;
			DisplayName = "Wants A and B";
			ParentTechIDs.AddRange(new List<TechIDs> { TechIDs.TestA, TechIDs.TestB } );
		}
	}
	
	/// <summary>
	/// Node with A as it's parent, and NOT b.
	/// </summary>
	public class NodeA2 : TestNode
	{
		public NodeA2()
		{
			TechID = TechIDs.TestA2;
			DisplayName = "Descendant of A";
			ParentTechIDs.Add( TechIDs.TestA );
		}
	}

}