using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLizzards.Data.CQRS.Contracts;

namespace TheLizzards.Data.CQRS.Entities
{
	public sealed class CommandBus : ICommandBus
	{
		private readonly IEnumerable<ICommandHandler> commandHandlers;

		private bool disposedValue = false;

		public CommandBus(IEnumerable<ICommandHandler> commandHandlers)
		{
			this.commandHandlers = commandHandlers;
		}

		public async Task Execute(ICommand command)
		{
			var applicableCommandHandlers = this.commandHandlers.Where(x => x.CanHandle(command));

			foreach (var handler in applicableCommandHandlers)
			{
				await handler.Execute(command);
			}
		}

		public void Dispose()
			=> Dispose(true);

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