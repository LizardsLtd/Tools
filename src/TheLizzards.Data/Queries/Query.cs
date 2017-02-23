using System;
using Serilog;
using TheLizzards.Data.CQRS.Contracts;
using TheLizzards.Data.CQRS.Entities;
using TheLizzards.Data.DDD.Contracts;

namespace TheLizzards.Data.Queries
{
	public abstract class SingleSetIdQuery<TResult>
			 : DataContextOperatorBase
			 //, IIdScoped<IAsyncQuery<TResult>>
			 , IsQuery
		 where TResult : IAggregateRoot
	{
		private readonly ILogger logger;
		private readonly string collectionType;

		public SingleSetIdQuery(IDataContext storageContext, ILoggerFactory loggerFactory, string collectionType)
			: this(storageContext, loggerFactory.CreateLogger(typeof(SingleSetIdQuery<TResult>)), collectionType)
		{
		}

		public SingleSetIdQuery(IDataContext storageContext, ILogger logger, string collectionType)
			: base(storageContext)
		{
			this.collectionType = collectionType;
			this.logger = logger;
		}

		public IAsyncQuery<TResult> SetId(Guid id)
			=> new Query(this.storageContext, this.logger, this.collectionType, id);

		private sealed class Query : SingleAsyncQueryBase<TResult>
		{
			private readonly IDataContext storageContext;
			private readonly ILogger logger;
			private readonly Guid id;
			private readonly string collectionType;

			public Query(IDataContext storageContext, ILogger logger, string collectionType, Guid id)
			{
				this.storageContext = storageContext;
				this.logger = logger;
				this.collectionType = collectionType;
				this.id = id;
			}

			public override Task<TResult> Execute()
				=> this.storageContext
					.Read<TResult>(Databases.Picums, this.collectionType)
					.Single(x => x.Id == this.id);
		}
	}
}