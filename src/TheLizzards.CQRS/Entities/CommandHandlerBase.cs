using System.Threading.Tasks;
using TheLizzards.CQRS.Contracts;

namespace TheLizzards.CQRS.Entities
{
	public abstract class CommandHandlerBase<TCommand> : ICommandHandler
		where TCommand : ICommand
	{
		private bool disposedValue = false;

		protected CommandHandlerBase()
		{
		}

		public bool CanHandle(ICommand command)
		{
			return command is TCommand;
		}

		public async Task Execute(ICommand command)
		{
			var castedCommand = (TCommand)command;

			await this.Execute(castedCommand);
		}

		public void Dispose() => Dispose(true);

		protected virtual void DisposeResources()
		{
		}

		protected abstract Task Execute(TCommand command);

		private void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					DisposeResources();
				}

				disposedValue = true;
			}
		}
	}
}