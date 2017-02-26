using System;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.CQRS.Queries;

namespace TheLizzards.Data.Tests.Mocks
{
	internal sealed class TestMaybeQuery : QueryByIdWithDefault<SimpleAggregateRoot>
	{
		public TestMaybeQuery(IDataContext storageContext, ILoggerFactory loggerFactory, DatabaseParts parts, Guid id)
			: base(storageContext, loggerFactory, parts, id)
		{
		}
	}
}