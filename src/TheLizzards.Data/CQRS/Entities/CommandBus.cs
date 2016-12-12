using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
			=> GetCommandsHandlers(command)
				.ToList()
				.ForEach(async handler => await handler.Execute(command));

		public IEnumerable<ValidationResult> Validate(ICommand command)
			=> GetCommandsHandlers(command)
				.SelectMany(handler => handler.Validate(command));

		public void Dispose() => Dispose(true);

		private IEnumerable<ICommandHandler> GetCommandsHandlers(ICommand command)
			=> this.commandHandlers.Where(x => x.CanHandle(command));

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