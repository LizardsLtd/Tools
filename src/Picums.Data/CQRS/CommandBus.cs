using System.Collections.Generic;
using System.Threading.Tasks;

namespace Picums.Data.CQRS
{
    public sealed class CommandBus : ICommandBus
    {
        private readonly IEnumerable<ICommandHandler> commandHandlers;

        private bool disposedValue = false;

        public CommandBus(IEnumerable<ICommandHandler> commandHandlers)
        {
            this.commandHandlers = commandHandlers;
        }

        public async Task Execute<TCommand>(TCommand command)
            where TCommand : ICommand
        {
            foreach (var handler in GetCommandsHandlers<TCommand>(command))
            {
                await handler.Handle(command);
            }
        }

        public void Dispose() => Dispose(true);

        private IEnumerable<ICommandHandler<TCommand>> GetCommandsHandlers<TCommand>(ICommand command)
            where TCommand : ICommand
        {
            foreach (var handler in this.commandHandlers)
            {
                if (this.CanHandle<TCommand>(handler))
                {
                    yield return (ICommandHandler<TCommand>)handler;
                }
            }
        }

        private bool CanHandle<TCommand>(ICommandHandler handler)
                where TCommand : ICommand
            => handler is ICommandHandler<TCommand>;

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var handler in commandHandlers)
                    {
                        handler.Dispose();
                    }
                }

                disposedValue = true;
            }
        }
    }
}