namespace Hexagon.Globals
{
	/// <summary>
	/// Holds immutable information about the game itself.
	/// </summary>
	public class GameInformation
	{
		public static readonly string GameName = "Hexagon";

		public static readonly string VersionString = "0.0.x";

		public static readonly string SourceCodeURL = "https://github.com/Neerti/HexagonGame";
		
		// Private constructor, to prevent initialization of this class outside of static.
		private GameInformation() { }
	}
}