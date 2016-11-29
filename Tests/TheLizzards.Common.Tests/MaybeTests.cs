using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLizzards.Common.Data;
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