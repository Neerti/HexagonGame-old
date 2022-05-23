using System;
using System.Linq;
using System.Reflection;
using Godot;
using Hexagon.Globals;

namespace Hexagon.Research.TechTrees
{
	/// <summary>
	/// This creates an instance of a specific <see cref="TechnologyTree"/>s, as well as the tree's nodes, and
	/// connects them together, returning a finished tree.
	/// </summary>
	public static class TechnologyTreeBuilder
	{
		public static TechnologyTree MakeTree(Type nodeType)
		{
			// Make a bare tree.
			var tree = new TechnologyTree();
			
			// Get all desired types via reflection.
			var assembly = Assembly.GetExecutingAssembly();
			var types = assembly.GetTypes();
			var subtypes = types.Where(x => x.IsSubclassOf(nodeType));
			
			// Instance them and add them to the list of nodes.
			foreach (var type in subtypes)
			{
				var newNode = (TechnologyNode)Activator.CreateInstance(type);
				if (newNode.TechID == TechIDs.Base)
					continue;
				tree.Nodes.Add(newNode.TechID, newNode);
				if (newNode.RootNode)
				{
					tree.RootNodes.Add(newNode);
				}
			}
			
			// Connect the nodes together.
			foreach (var entry in tree.Nodes)
			{
				foreach (var prerequisite in entry.Value.ParentTechIDs.Select(type => tree.Nodes[type]))
				{
					entry.Value.LinkToParent(prerequisite);
				}
			}

			return tree;
		}
	}
}
