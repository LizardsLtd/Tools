using System;
using Picums.Data.CQRS;

namespace Picums.Data.Tests.Mocks
{
    internal sealed class TestCommand : ICommand
    {
        public TestCommand()
        {
            CommandId = Guid.NewGuid();
        }

        public Guid CommandId { get; }
    }
}