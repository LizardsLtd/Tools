using System;
using System.Linq;
using System.Security;

namespace TheLizzards.CQRS.Azure.Entities
{
	public sealed class AzureDocumentDbOptions
	{
		public string Endpoint { get; set; }

		public string AuthKey { get; set; }

		public string Database { get; set; }

		public Uri EndpointUri => new Uri(this.Endpoint);

		public SecureString AuthenticationKey
			=> this.AuthKey
				.ToCharArray()
				.Aggregate(
					new SecureString(),
					(secureString, character) =>
					{
						secureString.AppendChar(character);
						return secureString;
					});
	}
}