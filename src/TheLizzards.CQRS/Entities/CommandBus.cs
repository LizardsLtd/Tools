using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheLizzards.Common.Data;
using TheLizzards.CQRS.Contracts;

namespace TheLizzards.CQRS.Entities
{
	public sealed class CommandBus : ICommandBus
	{
		private readonly IEnumerable<ICommandHandler> commandHandlers;

		private bool disposedValue = false;

		public CommandBus(IEnumerable<ICommandHandler> commandHandlers)
		{
			this.commandHandlers = commandHandlers;
		}

		public async Task<IResult> Execute(ICommand command)
		{
			var applicableCommandHandlers = this.commandHandlers.Where(x => x.CanHandle(command));

			foreach (var handler in applicableCommandHandlers)
			{
				var result = await handler.Execute(command);

				if (!result.IsSuccess)
				{
					return result;
				}
			}

			return Results.Success;
		}

		public void Dispose()
		{
			Dispose(true);
		}

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