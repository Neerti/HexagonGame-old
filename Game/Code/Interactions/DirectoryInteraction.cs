using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.ObjectModel;
using JetBrains.Annotations;

namespace Hexagon.Interactions
{
	/// <summary>
	/// Acts as a 'folder' for <see cref="Interaction"/> buttons. Can be nested.
	/// </summary>
	public class DirectoryInteraction : Interaction
	{
		/// <summary>
		/// Internal list of <see cref="Interaction"/>s contained within this one.
		/// </summary>
		private readonly List<Interaction> _subInteractions = new List<Interaction>();
		
		/// <summary>
		/// Nested <see cref="Interaction"/>s inside of this one, acting as a 'folder' for buttons.
		/// </summary>
		public ReadOnlyCollection<Interaction> SubInteractions => _subInteractions.AsReadOnly();

		/// <summary>
		/// Types of <see cref="Interaction"/>s to instantiate when this object is instantiated.
		/// </summary>
		protected readonly List<Type> SubInteractionTypes = new List<Type>();

		[UsedImplicitly]
		public DirectoryInteraction()
		{
			MakeSubInteractions();
		}

		private void MakeSubInteractions()
		{
			foreach (var type in SubInteractionTypes)
			{
				var newInteraction = (Interaction) Activator.CreateInstance(type);
				_subInteractions.Add(newInteraction);
			}
		}

		public override bool ShouldBeVisible()
		{
			return _subInteractions.Any(interaction => interaction.ShouldBeVisible());
		}
	}

	public class Sustenance : DirectoryInteraction
	{
		public Sustenance()
		{
			DisplayName = "Sustenance";
			TexturePath = "res://Code/UI/Icons/lorc/campfire.png";
			SubInteractionTypes.Add(typeof(Foraging));
		}
	}
	
	public class Foraging : DirectoryInteraction
	{
		public Foraging()
		{
			DisplayName = "Foraging";
			TexturePath = "res://Code/UI/Icons/lorc/falling-leaf.png";
		}
	}
	
	public class ForagingArea : Foraging
	{
		public ForagingArea()
		{
			DisplayName = "Foraging Area";
			TexturePath = "res://Code/UI/Icons/lorc/falling-leaf.png";
		}
	}
	
	
}