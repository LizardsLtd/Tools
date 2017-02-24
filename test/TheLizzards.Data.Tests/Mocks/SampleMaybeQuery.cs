using System;
using Serilog;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.Queries;

namespace TheLizzards.Data.Tests.Mocks
{
	internal sealed class SampleMaybeQuery : MaybeQueryById<SimpleAggregateRoot>
	{
		public SampleMaybeQuery(IDataContext storageContext, ILogger logger, DatabaseParts parts, Guid id)
			: base(storageContext, logger, parts, id)
		{
		}
	}
}