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

		//[Fact]
		//public void SingleOrNothiong()
		//{
		//	var collection = new List<int> { 1 };

		//	var result = collection.SingleOrNothing();

		//	Assert.True(result.IsSome);
		//}

		//[Fact]
		//public void SingleOrNothingWoithQuery()
		//{
		//	var result = this
		//		.exampleCollection
		//		.SingleOrNothing(x => x == 1);

		//	Assert.True(result.IsSome);
		//}

		//[Fact]
		//public void SingleOrNothiong()
		//{
		//	var collection = new List<int> { 1 };

		//	var result = collection.SingleOrNothing();

		//	Assert.True(result.IsSome);
		//}

		//[Fact]
		//public void SingleOrNothingWoithQuery()
		//{
		//	var result = this
		//		.exampleCollection
		//		.SingleOrNothing(x => x == 1);

		//	Assert.True(result.IsSome);
		//}
	}
}