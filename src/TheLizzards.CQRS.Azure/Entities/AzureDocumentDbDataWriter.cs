using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using TheLizzards.Common.Data;

namespace TheLizzards.CQRS.Azure.Entities
{
	internal class AzureDocumentDbDataWriter<T> : IDataWriter<T> where T : IAggregateRoot
	{
		private readonly DocumentClient client;
		private readonly string collectionUri;
		private readonly string databaseId;

		public AzureDocumentDbDataWriter(DocumentClient client, string databaseId, string collectionUri)
		{
			this.client = client;
			this.databaseId = databaseId;
			this.collectionUri = collectionUri;
		}

		public async Task Insert(T item) => await this.InsertDocumnet(item);

		public async Task Update(T item) => await InsertDocumnet(item, await QuertyForETag(item.Id));

		private async Task<string> QuertyForETag(Guid itemId)
		{
			var documentUri = UriFactory.CreateDocumentUri(
				this.databaseId
				, this.collectionUri
				, itemId.ToString());

			var document = await this.client.ReadDocumentAsync(documentUri);

			return document?.Resource?.ETag;
		}

		private async Task InsertDocumnet(T item, string etag = "")
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
	}
}