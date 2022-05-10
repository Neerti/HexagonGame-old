using System.Collections.Generic;

namespace Hexagon.Research.TechTrees
{
	/// <summary>
	/// An object which represents a specific 'node' inside a web or tree, 
	/// for the purposes of researching technologies.
	/// The actual tracking of whether any given tech is unlocked is done on a different object.
	/// </summary>
	public abstract class TechnologyNode
	{
		/// <summary>
		/// Internal identification enum for tech nodes. 
		/// Each 'real' node class used in the game should have a unique ID. 
		/// Subclasses used for inheritance (e.g. AITechnologyNode) should 
		/// instead keep their trait_id to Base.
		/// </summary>
		public TechIDs TechID { get; protected set; } = TechIDs.Base;
		
		/// <summary>
		/// The name of the technology, that is displayed to the player.
		/// </summary>
		public string DisplayName { get; protected set; }
		
		/// <summary>
		/// A one sentence summary of what the technology is about, shown in the UI.
		/// </summary>
		public string ShortDescription { get; protected set; }
		
		/// <summary>
		/// A longer string that describes the technology with more depth. Displayed to the player in the UI.
		/// </summary>
		public string Description { get; protected set; }
		
		/// <summary>
		/// Optional technobabble and lore goes here. Displayed to the player in the UI.
		/// </summary>
		public string Fluff { get; protected set; }

		/// <summary>
		/// An optional quote, displayed in the UI for flavor.
		/// </summary>
		public string Quote { get; protected set; }

		/// <summary>
		/// List of types of nodes which are prerequisites of this node.
		/// Used to connect this node to other nodes when the tree is being built.
		/// </summary>
		public List<TechIDs> ParentTechIDs { get; protected set; } = new List<TechIDs>();

		/// <summary>
		/// Designates that this node is intended to not have any parents.
		/// This is done to allow for testing to discover nodes which cannot be reached
		/// due to lacking a path to a root node, or to find root nodes that were accidentally given parents.
		/// Multiple root nodes can exist in a single tree, but all nodes must either be a root node or be able to
		/// route to a root node.
		/// </summary>
		public bool RootNode = false;

		/// <summary>
		/// List of instanced <see cref="TechnologyNode"/>s which come before this one.
		/// The list is built at runtime by the tech tree object.
		/// </summary>
		public List<TechnologyNode> Parents { get; protected set; } = new List<TechnologyNode>();

		/// <summary>
		/// List of instanced <see cref="TechnologyNode"/>s which branch off from this one.
		/// The list is built at runtime by the tech tree object.
		/// </summary>
		public List<TechnologyNode> Children { get; protected set; } = new List<TechnologyNode>();

		/// <summary>
		/// Connects this node to the input node in two directions. 
		/// Note that this node will be 'ahead' of the inputted node.
		/// </summary>
		/// <param name="node">The node that is a prerequisite to this node.</param>
		public void LinkToParent(TechnologyNode node)
		{
			Parents.Add(node);
			node.Children.Add(this);
		}
		
		
		public override string ToString()
		{
			return DisplayName;
		}
	}

	public enum TechIDs
	{
		Base,
		// Test nodes.
		TestRoot,
		TestA,
		TestB,
		TestAAndB,
		TestA2,
		// Science nodes.
		Speech,
		Language,
		Writing,
		Foraging,
		StoneKnapping,
		BoneWorking,
		Woodcutting,
		ToolMaking,
		Digging,
		Mining,
		Firemaking,
		Cooking,
		Ceramics
	}
}