using System;
using System.Collections;
using System.Collections.Generic;

namespace Hexagon.ECS.SparseSets
{
	/// <summary>
	/// Basic implementation of a sparse set collection, with the ability to contain objects, intended for use with ECS.
	/// </summary>
	/// <typeparam name="T">The type of object contained inside.</typeparam>
	public class SparseSet<T> : IEnumerable<T>
	{
		/// <summary>
		/// Array of indices for the <see cref="Dense"/> array, which can contain holes.
		/// </summary>
		public int[] Sparse;
		
		/// <summary>
		/// Array of indices for <see cref="Sparse"/>, that has no holes.
		/// </summary>
		public int[] Dense;
		
		/// <summary>
		/// Array that contains the elements directly.
		/// </summary>
		public T[] Elements;
		
		/// <summary>
		/// How many objects are contained inside of <see cref="Elements"/>, and by extension, inside of <see cref="Dense"/>.
		/// </summary>
		public int Count;
		
		/// <summary>
		/// Maximum capacity for this collection.
		/// </summary>
		public int Max;

		public SparseSet(int maxSize)
		{
			Sparse = new int[maxSize];
			Dense = new int[maxSize];
			Elements = new T[maxSize];
			Max = maxSize;
		}

		public T Get(int index)
		{
			return Elements[Sparse[index]];
		}

		public void Add(int index, T element)
		{
			// Duplicate indices are not allowed.
			if (Contains(index))
			{
				return;
			}

			// Check if out of bounds.
			if (index > Max || index < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			
			// Add the new element to the end.
			Elements[Count] = element;
			Dense[Count] = index;
			Sparse[index] = Count++;
		}

		public void Remove(int index)
		{
			if (!Contains(index))
			{
				return;
			}

			// Get the last element in the collection.
			var last = Dense[Count-1];
			
			// Replace the element being removed with the last element.
			Dense[Sparse[index]] = last;
			Elements[Sparse[index]] = Elements[last];
			
			// Update the index for the sparse array to point to where the removed element was.
			Sparse[last] = Sparse[index];

			// Decrement by one to complete the swap-remove.
			Count--;
		}

		public bool Contains(int index)
		{
			// Check if out of bounds.
			if (index > Max || index < 0)
			{
				throw new ArgumentOutOfRangeException();
			}

			return Sparse[index] < Count && Dense[Sparse[index]] == index;
		}

		public IEnumerator<T> GetEnumerator()
		{
			for (int i = 0; i < Count; i++)
			{
				yield return Elements[i];
			}
		}
		
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		
	}
}