﻿using System;
using Microsoft.Extensions.Logging;
using TheLizzards.Data.CQRS.Contracts.DataAccess;
using TheLizzards.Data.Queries;

namespace TheLizzards.Data.Tests.Mocks
{
	internal sealed class SampleMaybeQuery : QueryByIdWithDefault<SimpleAggregateRoot>
	{
		public SampleMaybeQuery(IDataContext storageContext, ILoggerFactory loggerFactory, DatabaseParts parts, Guid id)
			: base(storageContext, loggerFactory, parts, id)
		{
		}
	}
}