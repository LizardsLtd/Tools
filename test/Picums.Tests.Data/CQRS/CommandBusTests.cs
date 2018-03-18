using System.Threading.Tasks;
using Picums.Data.CQRS;
using Picums.Data.Tests.Mocks;
using Xunit;

namespace Picums.Data.Tests.CQRS
{
    public sealed class CommandBusTests
    {
        private readonly CommandBus commandBus;
        private readonly TestCommandHandler commandHandler;

        public CommandBusTests()
        {
            this.commandHandler = new TestCommandHandler();
            var handlers = new ICommandHandler[] { this.commandHandler };
            this.commandBus = new CommandBus(handlers);
        }

        [Fact]
        public async Task CommandBusCanCastHandlersToRequiredTypes()
        {
            var command = new TestCommand();

            await this.commandBus.Execute(command);

            this.commandHandler.HandleHasBeenRequestedOnce();
        }
    }
}