using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TheLizzards.Maybe.Tests
{
	public sealed class MaybeForCollectionTests
	{
		private readonly List<int> exampleCollection;

		public MaybeForCollectionTests()
		{
			this.exampleCollection = new List<int>
			{
				1,
				2,
				3,
				4,
				5,
			};
		}

		[Fact]
		public void CastToMaybeCollectionWorks()
		{
			var collection = this.exampleCollection.ToMaybeList();

            Assert.True(collection.All(x => x.IsSome));
		}
	}
}