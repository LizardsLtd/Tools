using Should.Fluent;
using Xunit;

namespace Picums.Maybe.Tests
{
    public sealed class MaybExtensionsTesteTests
    {
        [Fact]
        public void NoneExtensionReturnsMaybeNothing()
        {
            Maybe<string> maybe = Maybe.None<string>();

            maybe.IsNone.Should().Be.True();
        }

        [Fact]
        public void FromExtensionRestunsMaybe()
        {
            Maybe<string> maybe = Maybe.From<string>("example");

            maybe.IsSome.Should().Be.True();
        }
    }
}