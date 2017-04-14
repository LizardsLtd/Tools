﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Logging;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;
using Picums.Maybe;

namespace Picums.Data.Azure
{
    internal sealed class AzureDocumentDbDataReader<T> : IDataReader<T>
        where T : IAggregateRoot
    {
        private readonly DocumentClient client;
        private readonly Uri collectionUri;
        private readonly ILogger logger;

        public AzureDocumentDbDataReader(
            DocumentClient client
            , Uri collectionUri
            , ILogger logger)
        {
            this.client = client;
            this.collectionUri = collectionUri;
            this.logger = logger;
        }

        public Task<IEnumerable<T>> All()
        {
            this.logger.LogInformation($"AzureDocumentDbDataReader for {typeof(T).Name} All function");
            return Task.FromResult<IEnumerable<T>>(this.QueryDocumentDb().ToArray());
        }

        public Task<TResult> QueryFor<TResult>(Expression<Func<IQueryable<T>, TResult>> predicate)
        {
            this.logger.LogInformation($"AzureDocumentDbDataReader for {typeof(T).Name} QueryFor function");
            var results = predicate.Compile().Invoke(this.QueryDocumentDb());
            return Task.FromResult(results);
        }

        public Task<Maybe<T>> SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            this.logger.LogInformation($"AzureDocumentDbDataReader for {typeof(T).Name} Where function");
            var result = this.QueryDocumentDb().Where(predicate.Compile()).ToArray();
            return Task.FromResult((Maybe<T>)result.FirstOrDefault());
        }

        public Task<IQueryable<T>> Where(Expression<Func<T, bool>> predicate)
        {
            this.logger.LogInformation($"AzureDocumentDbDataReader for {typeof(T).Name} Where function");
            return Task.FromResult(this.QueryDocumentDb().Where(predicate.Compile()).AsQueryable());
        }

        private IOrderedQueryable<T> QueryDocumentDb()
            => client.CreateDocumentQuery<T>(collectionUri);
    }
}