//using System;
//using System.Collections.Generic;
//using System.Text;
//using Picums.Data.CQRS.Contracts.DataAccess;

//namespace Picums.Data.CQRS.Queries
//{
//	public abstract class SingleSetIdQuery<TResult>
//			: DataContextOperatorBase
//			, IIdScoped<IAsyncQuery<Maybe<TResult>>>
//			, IsQuery
//		where TResult : IAggregateRoot
//	{
//		private readonly ILogger logger;
//		private readonly string collectionType;

//		public SingleSetIdQuery(IDataContext storageContext, ILoggerFactory loggerFactory, string collectionType)
//			: this(storageContext, loggerFactory.CreateLogger<FindCategoryGroupById>(), collectionType)
//		{
//		}

//		public SingleSetIdQuery(IDataContext storageContext, ILogger logger, string collectionType)
//			: base(storageContext)
//		{
//			this.collectionType = collectionType;
//			this.logger = logger;
//		}
//		public IAsyncQuery<Maybe<TResult>> SetId(Guid id)
//			=> new Query(this.storageContext, this.logger, this.collectionType, id);

//		private sealed class Query : SingleAsyncQueryBase<Maybe<TResult>>
//		{
//			private readonly IDataContext storageContext;
//			private readonly ILogger logger;
//			private readonly Guid id;
//			private readonly string collectionType;

//			public Query(IDataContext storageContext, ILogger logger, string collectionType, Guid id)
//			{
//				this.storageContext = storageContext;
//				this.logger = logger;
//				this.collectionType = collectionType;
//				this.id = id;
//			}

//			public override Task<Maybe<TResult>> Execute()
//				=> this.storageContext
//					.Read<TResult>(Databases.Picums, this.collectionType)
//					.SingleOrDefault(x => x.Id == this.id);
//		}
//	}

//}