using Microsoft.Extensions.Logging;
using TheLizzards.Localisation.Entities;
using TheLizzards.Tests;
using Xunit;

namespace TheLizzards.Location.Tests.Entities
{
	public sealed class GoogleGeocodingResultsTests
	{
		private readonly string exampleFile;
		private readonly ILogger emptyLogger;

		public GoogleGeocodingResultsTests()
		{
			var embededResources = new EmbeddedContentLoader("TheLizzards.Location.Tests");
			this.exampleFile = embededResources
				.LoadTextFileAsync("TheLizzards.Location.Tests.Resources.GoogleServiceResluts.xml")
				.GetAwaiter()
				.GetResult();
			this.emptyLogger = new TestLoggerFactory().CreateLogger("null");
		}

		[Fact]
		public void ParseServiceResult()
		{
			var parser = new GoogleGeocodingResults(this.emptyLogger, this.exampleFile);

			Assert.True(parser.HasResults);
		}
	}
}