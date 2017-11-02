using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Microsoft.Extensions.Configuration;

namespace Picums.Data.Azure
{
    public sealed class AzureDocumentDbOptions
    {
        public string Endpoint { get; set; }

        public string AuthKey { get; set; }

        public IEnumerable<AzureDatabase> Databases { get; set; }

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

        public static void FromConfiguration(IConfiguration config, IEnumerable<string> collections, AzureDocumentDbOptions options)
        {
            options.Databases = config
                .GetSection("ConnectionString:Databases")
                .GetChildren()
                .Select(x => new
                {
                    Name = x["Name"],
                })
                .Select(x => new AzureDatabase(x.Name, collections));
            options.Endpoint = config["ConnectionString:AccountEndpoint"];
            options.AuthKey = config["ConnectionString:AccountKey"];
        }
    }
}