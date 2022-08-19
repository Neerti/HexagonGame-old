using Xunit;
using Hexagon.Map;

namespace Hexagon.Tests
{
	public class VectorHexTests
	{
		// Equals()
		[Fact]
		public void Equality_ComparingSameCoordinates_ShouldBeEqual()
		{
			var a = new VectorHex(1, -1, 0);
			var b = new VectorHex(1, -1, 0);
			
			Assert.Equal(a, b);
		}
		
		[Fact]
		public void Equality_ComparingDifferentCoordinates_ShouldNotBeEqual()
		{
			var a = new VectorHex(1, -1, 0);
			var b = new VectorHex(2, -2, 0);
			
			Assert.NotEqual(a, b);
		}
		
		[Fact]
		public void Equality_CubicVsCartesian_ShouldBeEqual()
		{
			var a = new VectorHex(5, -3, -2);
			var b = new VectorHex(5, -1);
			
			Assert.Equal(a, b);
		}
		
		// Add()
		[Fact]
		public void Addition_AddingTwoVectors_ShouldBeEqual()
		{
			var a = new VectorHex(2, -2, 0);
			var b = new VectorHex(2, -2, 0);
			var expected = new VectorHex(4, -4, 0);
			
			Assert.Equal(expected, a + b);
		}
		
		// Subtract()
		[Fact]
		public void Subtraction_SubtractingTwoVectors_ShouldBeEqual()
		{
			var a = new VectorHex(2, -2, 0);
			var b = new VectorHex(2, -2, 0);
			var expected = new VectorHex(4, -4, 0);
			
			Assert.Equal(expected, a + b);
		}
		
		// Multiply()
		[Fact]
		public void Multiplication_MultiplyingTwoVectors_ShouldBeEqual()
		{
			var a = new VectorHex(2, -2, 0);
			var expected = new VectorHex(4, -4, 0);
			
			Assert.Equal(expected, a * 2);
		}
	}
}