
using System.Collections.Generic;

namespace Hexagon.Items.Recipes
{
	/// <summary>
	/// A Recipe is a simple object which holds data that determines what is needed to turn <see cref="Item"/>s into
	/// different Items. 
	/// </summary>
	public abstract class Recipe
	{
		/// <summary>
		/// The name of the recipe, shown in the UI.
		/// </summary>
		protected string Name;

		/// <summary>
		/// Contains the required ingredients for producing the output items, which will be consumed.
		/// The value is the quantity required.
		/// </summary>
		protected Dictionary<ItemIDs, int> Inputs;

		/// <summary>
		/// Contains the result of the recipe after successfully using it.
		/// The value is the quantity gained.
		/// </summary>
		protected Dictionary<ItemIDs, int> Outputs;

	}
}