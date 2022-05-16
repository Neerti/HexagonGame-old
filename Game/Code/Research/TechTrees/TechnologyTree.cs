using System;
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

		public List<TechnologyNode> RootNodes = new List<TechnologyNode>();

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
		
		public List<TechnologyNode> GetAllChildrenOfNode(TechnologyNode startingNode)
		{
			return RecursiveChildren(startingNode, new List<TechnologyNode>());
		}
		
		List<TechnologyNode> RecursiveChildren(TechnologyNode startingNode, List<TechnologyNode> childrenFound)
		{
			childrenFound.AddRange(startingNode.Parents);
			foreach (var parent in startingNode.Parents)
			{
				return RecursiveChildren(parent, childrenFound);
			}
			return childrenFound;
		}

		/// <summary>
		/// Calculates the number of steps between two nodes in the tree.
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public int DistanceBetweenNodes(TechnologyNode start, TechnologyNode end)
		{
			// Looking upwards.
			if (GetAllParentsOfNode(start).Contains(end))
			{
				return RecursiveParentDistance(start, end);
			}
			// Otherwise we look downwards.
			if (GetAllChildrenOfNode(start).Contains(end))
			{
				throw new NotImplementedException();
			}
			// Nodes can't be reached.
			return -1;
		}

		private int RecursiveParentDistance(TechnologyNode currentNode, TechnologyNode targetNode, int currentDistance = 0)
		{
			if (currentNode != targetNode)
			{
				foreach (var parent in currentNode.Parents)
				{
					return RecursiveParentDistance(parent, targetNode, ++currentDistance);
				}
			}
			return currentDistance;
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