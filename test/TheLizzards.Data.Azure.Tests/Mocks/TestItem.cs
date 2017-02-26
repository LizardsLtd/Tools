using System;
using Newtonsoft.Json;
using TheLizzards.Data.DDD;

namespace TheLizzards.Data.Azure.Tests.Mocks
{
	internal sealed class TestItem : IAggregateRoot
	{
		public TestItem() => this.Id = Guid.Parse("75862391-0fa9-4b58-9d03-702a7c1d3ff1");

		[JsonProperty(PropertyName = "id")]
		public Guid Id { get; }
	}
}