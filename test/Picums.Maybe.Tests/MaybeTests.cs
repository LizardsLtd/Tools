using Xunit;
using Should.Fluent;

namespace Picums.Maybe.Tests
{
	public sealed class MaybeTests
	{
		[Fact]
		public void MaybeIsNeverNull()
		{
			Maybe<string> possibleMaybe = null;

            possibleMaybe.Should().Not.Be.Null();
		}

		[Fact]
		public void NullCastedMaybeIsNone()
		{
			Maybe<string> maybe = null;

            maybe.IsNone.Should().Be.True();
		}

		[Fact]
		public void NullCastedMaybeIsNotSome()
		{
			Maybe<string> maybe = null;

            maybe.IsSome.Should().Be.False();
        }

		[Fact]
		public void ValueCastedMaybeIsNotNone()
		{
			var maybe = Maybe.From("test");

            maybe.IsNone.Should().Be.False();
        }

		[Fact]
		public void ValueCastedMaybeIsSome()
		{
			var maybe = Maybe.From("test");

            maybe.IsSome.Should().Be.True();
        }

        [Fact]
		public void CreateMaybeUsingFrom()
		{
			var maybe = Maybe.From("test");

            maybe.IsSome.Should().Be.True();
		}

		[Theory]
		[InlineData(5, 3, 1)]
		[InlineData(5, 8, -1)]
		[InlineData(5, 5, 0)]
		public void ComparisionWorksForPayload(int value, int compareTo, int result)
		{
			var maybe = Maybe.From(value);

			var comparisionValue = maybe.CompareTo(compareTo);

            result.Should().Be.Equals(comparisionValue);
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

            result.Should().Be.Equals(comparisionValue);
        }

		[Theory]
		[InlineData(5, 5, true)]
		[InlineData(5, 8, false)]
		public void EqualityWorksForPayload(int value, int compareTo, bool result)
		{
			var maybe = Maybe.From(value);

			var comparisionValue = maybe.Equals(compareTo);
            
            result.Should().Be.Equals(comparisionValue);
        }

		[Theory]
		[InlineData(5, 5, true)]
		[InlineData(5, 8, false)]
		public void EqualityWorksForMaybes(int value, int compareTo, bool result)
		{
			var maybe = Maybe.From(value);

			var comparisionValue = maybe.Equals(compareTo);
            
            result.Should().Be.Equals(comparisionValue);
        }
	}
}