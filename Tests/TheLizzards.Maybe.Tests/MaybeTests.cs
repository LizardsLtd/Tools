using System;
using System.Linq;
using FluentAssertions;
using TheLizzards.Maybe;
using Xunit;

namespace TheLizzards.Maybe.Tests
{
	public sealed class MaybeTests
	{
		[Fact]
		public void MaybeIsNeverNull()
		{
			Maybe<string> possibleMaybe = null;

			Assert.NotNull(possibleMaybe);
		}

		[Theory]
		[InlineData(5, 3, 1)]
		[InlineData(5, 8, -1)]
		[InlineData(5, 5, 0)]
		public void ComparisionWorksForPayload(int value, int compareTo, int result)
		{
			var maybe = Maybe.From(value);

			var comparisionValue = maybe.CompareTo(compareTo);

			comparisionValue.Should().Be(result);
		}

		[Theory]
		[InlineData(5, 3, 1)]
		[InlineData(5, 8, -1)]
		[InlineData(5, 5, 0)]
		public void ComparisionWorksForMaybes(int value, int compareTo, int result)
		{
			var maybe = Maybe.From(value);
			var other = Maybe.From(compareTo);

			var comparisionValue = maybe.CompareTo(other);

			comparisionValue.Should().Be(result);
		}

		[Theory]
		[InlineData(5, 5, true)]
		[InlineData(5, 8, false)]
		public void EqualityWorksForPayload(int value, int compareTo, bool result)
		{
			var maybe = Maybe.From(value);

			var comparisionValue = maybe.Equals(compareTo);

			comparisionValue.Should().Be(result);
		}

		[Theory]
		[InlineData(5, 5, true)]
		[InlineData(5, 8, false)]
		public void EqualityWorksForMaybes(int value, int compareTo, bool result)
		{
			var maybe = Maybe.From(value);

			var comparisionValue = maybe.Equals(compareTo);

			comparisionValue.Should().Be(result);
		}
	}
}