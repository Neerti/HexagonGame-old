using System;
using System.Collections.Generic;

namespace Hexagon.Map
{
	/// <summary>
	/// Virtual structure that represents a specific coordinate in a hexagonal grid,
	/// unbound to any particular map implementation. Based off of
	/// https://www.redblobgames.com/grids/hexagons
	/// </summary>
	public readonly struct VectorHex : IEquatable<VectorHex>
	{
		/// <summary>
		/// Creates a new VectorHex struct.
		/// </summary>
		/// <param name="q"></param>
		/// <param name="r"></param>
		/// <param name="s"></param>
		/// <exception cref="ArgumentException">All three arguments must sum to zero to be valid.</exception>
		public VectorHex(int q, int r, int s)
		{
			Q = q;
			R = r;
			S = s;

			X = q;
			Y = r + (q - (q & 1)) / 2;
			
			if (q + r + s != 0)
			{
				throw new ArgumentException("(Q + R + S) must equal zero.");
			}
		}

		public VectorHex(int x, int y)
		{
			X = x;
			Y = y;
			
			Q = x;
			R = y - (x - (x & 1)) / 2;
			S = -Q - R;

			if (Q + R + S != 0)
			{
				throw new ArgumentException("(Q + R + S) must equal zero.");
			}
		}

		/// <summary>
		/// The first cubic coordinate.
		/// </summary>
		public int Q { get; }
		
		/// <summary>
		/// The second cubic coordinate.
		/// </summary>
		public int R { get; }
		
		/// <summary>
		/// The third cubic coordinate.
		/// </summary>
		public int S { get; }
		
		/// <summary>
		/// The first cartesian coordinate.
		/// </summary>
		public int X { get; }
		
		/// <summary>
		/// The second cartesian coordinate.
		/// </summary>
		public int Y { get; }

		public static readonly List<VectorHex> AdjacentHexes = new List<VectorHex>
		{
			new VectorHex(1, 0, -1), new VectorHex(1, -1, 0), new VectorHex(0, -1, 1),
			new VectorHex(-1, 0, 1), new VectorHex(-1, 1, 0), new VectorHex(0, 1, -1)
		};
		
		public static VectorHex Adjacent(int direction)
		{
			return AdjacentHexes[direction];
		}
		
		public override string ToString()
		{
			return $@"VectorHex (Cubic: {Q}, {R}, {S}) (Cartesian: {X}, {Y})";
		}

		// Equality.
		public bool Equals(VectorHex other)
		{
			return Q == other.Q && R == other.R && S == other.S;
		}

		public override bool Equals(object obj)
		{
			return obj is VectorHex other && Equals(other);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = Q;
				hashCode = (hashCode * 397) ^ R;
				hashCode = (hashCode * 397) ^ S;
				return hashCode;
			}
		}

		public static bool operator ==(VectorHex a, VectorHex b) => a.Equals(b);

		public static bool operator !=(VectorHex a, VectorHex b) => !(a == b);

		
		// Arithmetic.
		public static VectorHex operator +(VectorHex addendA, VectorHex addendB)
		{
			return new VectorHex(
				addendA.Q + addendB.Q,
				addendA.R + addendB.R,
				addendA.S + addendB.S
				);
		}

		public static VectorHex operator -(VectorHex minuend, VectorHex subtrahend)
		{
			return new VectorHex(
				minuend.Q - subtrahend.Q,
				minuend.R - subtrahend.R,
				minuend.S - subtrahend.S
			);
		}

		public static VectorHex operator *(VectorHex multiplier, int multiplicand)
		{
			return new VectorHex(
				multiplier.Q * multiplicand,
				multiplier.R * multiplicand,
				multiplier.S * multiplicand
			);
		}

		public static VectorHex operator /(VectorHex dividend, int divisor)
		{
			return new VectorHex(
				dividend.Q / divisor,
				dividend.R / divisor,
				dividend.S / divisor
			);
		}

		/// <summary>
		///	Returns a new <see cref="VectorHex"/> that is rotated 60 degrees counter-clockwise.
		/// </summary>
		/// <returns>The rotated vector.</returns>
		public VectorHex RotateLeft()
		{
			return new VectorHex(-S, -Q, -R);
		}

		/// <summary>
		///	Returns a new <see cref="VectorHex"/> that is rotated 60 degrees clockwise.
		/// </summary>
		/// <returns>The rotated vector.</returns>
		public VectorHex RotateRight()
		{
			return new VectorHex(-R, -S, -Q);
		}
		
		public int Length()
		{
			return (Math.Abs(Q) + Math.Abs(R) + Math.Abs(S)) / 2;
		}

		public int Distance(VectorHex other)
		{
			return (this - other).Length();
		}

	}
	
	
}