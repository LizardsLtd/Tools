using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Picums.Maybe.Tests
{
	public sealed class MaybeForCollectionTests
	{
		private readonly List<int> exampleCollection;

		public MaybeForCollectionTests()
		{
			this.exampleCollection = new List<int> { 1, 2, 3, 4, 5, };
		}

		[Fact]
		public void CastToMaybeCollectionWorks()
		{
			var collection = this.exampleCollection.ToMaybeList();

			Assert.True(collection.All(x => x.IsSome));
		}

		[Fact]
		public void SingleOrNothiong()
		{
			var collection = new List<int> { 1 };

			var result = collection.SingleOrNothing();

			Assert.True(result.IsSome);
		}

		[Fact]
		public void SingleOrNothingWoithQuery()
		{
			var result = this
				.exampleCollection
				.SingleOrNothing(x => x == 1);

			Assert.True(result.IsSome);
		}

		[Fact]
		public void FirstOrNothiong()
		{
			var collection = new List<int> { 1 };

			var result = collection.FirstOrNothing();

			Assert.True(result.IsSome);
		}

		[Fact]
		public void FirstOrNothingWoithQuery()
		{
			var result = this
				.exampleCollection
				.FirstOrNothing(x => x == 1);

			Assert.True(result.IsSome);
		}

		[Fact]
		public void LastOrNothiong()
		{
			var collection = new List<int> { 1 };

			var result = collection.LastOrNothing();

			Assert.True(result.IsSome);
		}

		[Fact]
		public void LastOrNothingWoithQuery()
		{
			var result = this
				.exampleCollection
				.LastOrNothing(x => x == 1);

			Assert.True(result.IsSome);
		}
	}
}