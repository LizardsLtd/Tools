using System;
using System.Linq;
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
	}
}