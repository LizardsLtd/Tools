﻿using System;
using System.Collections.Generic;
using System.Linq;
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

        public AzureDocumentDbDataReader(DocumentClient client, Uri collectionUri, ILogger logger)
        {
            this.client = client;
            this.collectionUri = collectionUri;
            this.logger = logger;
        }

        public Task<IQueryable<T>> Collection(Func<T, bool> filter)
        {
            this.logger.LogInformation($"AzureDocumentDbDataReader for {typeof(T).Name} All function");
            var result = this.client
                .CreateDocumentQuery<T>(this.collectionUri)
                .Where(filter)
                .AsQueryable();

            return Task.FromResult(result);
        }

        public Task<Maybe<T>> Single(Func<T, bool> filter, Func<IEnumerable<T>, T> reduce)
        {
            this.logger.LogInformation($"AzureDocumentDbDataReader for {typeof(T).Name} All function");
            var collection = this.client
                .CreateDocumentQuery<T>(this.collectionUri)
                .Where(filter);

            var result = reduce(collection);

            return Task.FromResult<Maybe<T>>(result);
        }
    }
}