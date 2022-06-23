using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hexagon.Globals
{
	public static class Helpers
	{
		/// <summary>
		/// Obtains all subclasses of a given type.
		/// </summary>
		/// <returns>An <see cref="IEnumerable{T}"/> containing all subclasses 
		/// of the inputted type.</returns>
		/// <param name="base_type">Type to find subtypes of.</param>
		public static IEnumerable<Type> SubtypesOf(Type base_type)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var types = assembly.GetTypes();
			return types.Where(t => t.IsSubclassOf(base_type));
		}
		
		/// <summary>
		/// Gets a list of instantiated subtypes for a given type.
		/// </summary>
		/// <returns>A list of instanced subtypes.</returns>
		/// <param name="base_type">Type to instance subtypes of.</param>
		public static List<object> InstancedSubtypesOf(Type base_type)
		{
			var types_to_instantiate = SubtypesOf(base_type);
			return types_to_instantiate.Select(Activator.CreateInstance).ToList();
		}

	}
}