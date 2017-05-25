using System;
using Picums.Data.Domain;

namespace Picums.Data.Tests.Mocks
{
    internal sealed class SimpleAggregateRoot : IAggregateRoot
    {
        public SimpleAggregateRoot(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}