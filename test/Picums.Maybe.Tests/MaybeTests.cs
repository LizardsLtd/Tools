using Xunit;

namespace Picums.Maybe.Tests
{
	public sealed class MaybeTests
	{
		[Fact]
		public void MaybeIsNeverNull()
		{
			Maybe<string> possibleMaybe = null;

			Assert.NotNull(possibleMaybe);
		}

		[Fact]
		public void NullCastedMaybeIsNone()
		{
			Maybe<string> maybe = null;

			Assert.True(maybe.IsNone);
		}

		[Fact]
		public void NullCastedMaybeIsNotSome()
		{
			Maybe<string> maybe = null;

			Assert.False(maybe.IsSome);
		}

		[Fact]
		public void ValueCastedMaybeIsNotNone()
		{
			var maybe = Maybe.From("test");

			Assert.False(maybe.IsNone);
		}

		[Fact]
		public void ValueCastedMaybeIsSome()
		{
			var maybe = Maybe.From("test");

			Assert.True(maybe.IsSome);
		}

		[Fact]
		public void CreateMaybeUsingFrom()
		{
			var maybe = Maybe.From("test");

			Assert.True(maybe.IsSome);
		}

		[Theory]
		[InlineData(5, 3, 1)]
		[InlineData(5, 8, -1)]
		[InlineData(5, 5, 0)]
		public void ComparisionWorksForPayload(int value, int compareTo, int result)
		{
			var maybe = Maybe.From(value);

			var comparisionValue = maybe.CompareTo(compareTo);

			Assert.Equal(result, comparisionValue);
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

			Assert.Equal(result, comparisionValue);
		}

		[Theory]
		[InlineData(5, 5, true)]
		[InlineData(5, 8, false)]
		public void EqualityWorksForPayload(int value, int compareTo, bool result)
		{
			var maybe = Maybe.From(value);

			var comparisionValue = maybe.Equals(compareTo);

			Assert.Equal(result, comparisionValue);
		}

		[Theory]
		[InlineData(5, 5, true)]
		[InlineData(5, 8, false)]
		public void EqualityWorksForMaybes(int value, int compareTo, bool result)
		{
			var maybe = Maybe.From(value);

			var comparisionValue = maybe.Equals(compareTo);

			Assert.Equal(result, comparisionValue);
		}
	}
}