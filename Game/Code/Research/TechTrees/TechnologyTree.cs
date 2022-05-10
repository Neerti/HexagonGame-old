using System.Collections.Generic;
using System.Linq;

namespace Hexagon.Research.TechTrees
{
	/// <summary>
	/// An object which contains and manages many <see cref="TechnologyNode"/> objects, and acts as the
	/// representation of a tech tree. 
	/// Note that TechnologyNodes have nothing to do with Godot nodes. 
	/// Another note is that this class is NOT a regular tree data structure (because multiple parents are possible),
	/// and is more properly defined as a directed acyclic graph.
	/// </summary>
	public class TechnologyTree
	{
		/// <summary>
		/// The name of the tech tree, that is displayed to the player. Multiple tech trees might exist in the game,
		/// so this allows for distinguishing between different ones.
		/// </summary>
		public string DisplayName { get; protected set; }

		/// <summary>
		/// Dictionary of all nodes in the tree, indexed by their type.
		/// </summary>
		public Dictionary<TechIDs, TechnologyNode> Nodes = new Dictionary<TechIDs, TechnologyNode>();

		public TechnologyNode GetNode(TechIDs id)
		{
			if (Nodes.ContainsKey(id))
			{
				return Nodes[id];
			}

			return null;
		}
		
		/// <summary>
		/// Gets a list of all ancestors of a node.
		/// </summary>
		/// <returns>All ancestors of the inputted node.</returns>
		/// <param name="startingNode">Node to walk up the tree from.</param>
		public List<TechnologyNode> GetAllParentsOfNode(TechnologyNode startingNode)
		{
			return RecursiveParents(startingNode, new List<TechnologyNode>());
		}
		
		List<TechnologyNode> RecursiveParents(TechnologyNode startingNode, List<TechnologyNode> parentsFound)
		{
			parentsFound.AddRange(startingNode.Parents);
			foreach (var parent in startingNode.Parents)
			{
				return RecursiveParents(parent, parentsFound);
			}
			return parentsFound;
		}
		
		/// <summary>
		/// Checks if the supplied node can be reached by any root node. 
		/// Unreachable nodes mean they likely cannot be researched.
		/// </summary>
		/// <returns><c>true</c>, if node is unreachable, <c>false</c> otherwise.</returns>
		/// <param name="testedNode">Node to determine if unreachable.</param>
		public bool IsNodeUnreachable(TechnologyNode testedNode)
		{
			if(testedNode.RootNode)
			{
				// We're the root!
				return false;
			}

			return GetAllParentsOfNode(testedNode).All(node => !node.RootNode);
		}

		public List<TechnologyNode> GetUnreachableNodes()
		{
			var unreachableNodes = new List<TechnologyNode>();
			foreach (var pair in Nodes)
			{
				if (IsNodeUnreachable(pair.Value))
				{
					unreachableNodes.Add(pair.Value);
				}
			}

			return unreachableNodes;
		}

		public List<TechnologyNode> GetSelfReferencingNodes()
		{
			var loopingNodes = new List<TechnologyNode>();
			foreach (var pair in Nodes)
			{
				if (pair.Value.ParentTechIDs.Contains(pair.Value.TechID))
				{
					loopingNodes.Add(pair.Value);
				}
			}

			return loopingNodes;
		}


		public override string ToString()
		{
			return DisplayName;
		}
	}
}