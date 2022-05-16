using Hexagon.Research.TechTrees;
using Hexagon.Research.TechTrees.ScienceTree;

namespace Hexagon.Globals
{
	public class Singleton
	{
		public static readonly TechnologyTree ScienceTechTree = TechnologyTreeBuilder.MakeTree(typeof(ScienceNode));
		
		// Private constructor, to prevent initialization of this class outside of static.
		private Singleton() { }
		
	}
}