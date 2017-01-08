using System;
using System.Linq;

namespace TheLizzards.Data.Azure.Entities
{
	public sealed class AzureAttributes
	{
		public AzureAttributes(object[] arguments)
			: this(
				 arguments[0].ToString()
				 , arguments[1].ToString())
		{
		}

		public AzureAttributes(string databaseId, string collectionName)
		{
			this.DatabaseId = databaseId;
			this.CllectionName = collectionName;
		}

		public string DatabaseId { get; }

		public string CllectionName { get; }

		public static implicit operator AzureAttributes(object[] arguments)
			=> new AzureAttributes(arguments);
	}
}