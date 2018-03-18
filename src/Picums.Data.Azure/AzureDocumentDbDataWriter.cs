﻿using System;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using NLog;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Domain;

namespace Picums.Data.Azure
{
    internal class AzureDocumentDbDataWriter<T>
        : IDataWriter<T>
             where T : IAggregateRoot
    {
        private readonly DocumentClient client;
        private readonly string collectionUri;
        private readonly string databaseId;
        private readonly ILogger logger;

        public AzureDocumentDbDataWriter(DocumentClient client, string databaseId, string collectionUri, ILogger logger)
        {
            this.client = client;
            this.databaseId = databaseId;
            this.collectionUri = collectionUri;
            this.logger = logger;
        }

        public async Task InsertNew(T item)
            => await this.InsertDocument(item);

        public async Task UpdateExisting(T item)
            => await InsertDocument(item, await QuertyForETag(item.Id));

        private async Task<string> QuertyForETag(Guid itemId)
        {
            var documentUri = UriFactory.CreateDocumentUri(
                this.databaseId
                , this.collectionUri
                , itemId.ToString());

            var document = await this.client.ReadDocumentAsync(documentUri);

            return document?.Resource?.ETag;
        }

        private async Task InsertDocument(T item, string etag = "")
        {
            try
            {
                var requestOptions = new RequestOptions();

                if (!string.IsNullOrEmpty(etag))
                {
                    requestOptions.AccessCondition = new AccessCondition
                    {
                        Condition = etag,
                        Type = AccessConditionType.IfMatch,
                    };
                }

                await this.client.UpsertDocumentAsync(
                      UriFactory.CreateDocumentCollectionUri(databaseId, collectionUri)
                      , item
                      , requestOptions);
            }
            catch (DocumentClientException exp)
            {
                this.logger.Error(exp.Message);
            }
        }
    }
}