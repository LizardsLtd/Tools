using System;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.DataAccess;

using TheLizzards.Data.CQRS.Queries;

namespace TheLizzards.Data.Tests.Mocks
{
	internal sealed class TestMaybeQuery : QueryById<SimpleAggregateRoot>
	{
		public TestMaybeQuery(IDataContext dataContext, ILoggerFactory loggerFactory, DatabaseParts parts, Guid id)
			: base(dataContext, loggerFactory, parts, id)
		{
		}
	}
}