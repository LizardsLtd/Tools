using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.DataAccess;
using TheLizzards.Data.CQRS.Queries;

namespace TheLizzards.Data.Tests.Mocks
{
	internal sealed class TestGetAllQuery : QueryForAll<SimpleAggregateRoot>
	{
		public TestGetAllQuery(IDataContext dataContext, ILoggerFactory loggerFactory, DatabaseParts parts)
			: base(dataContext, loggerFactory, parts)
		{
		}
	}
}