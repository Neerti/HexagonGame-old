namespace Hexagon.Research.TechTrees.ScienceTree
{
	/// <summary>
	/// Base class for nodes that are part of the science tree.
	/// </summary>
	public class ScienceNode : TechnologyNode { }

	public class Speech : ScienceNode
	{
		public Speech()
		{
			TechID = TechIDs.Speech;
			DisplayName = "Speech";
			RootNode = true;
		}
	}
	
	public class Language : ScienceNode
	{
		public Language()
		{
			TechID = TechIDs.Language;
			DisplayName = "Language";
			ParentTechIDs.Add( TechIDs.Speech );
		}
	}
	
	public class Writing : ScienceNode
	{
		public Writing()
		{
			TechID = TechIDs.Writing;
			DisplayName = "Writing";
			ParentTechIDs.Add( TechIDs.Language );
		}
	}
}