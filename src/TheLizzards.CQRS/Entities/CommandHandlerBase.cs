using System.Threading.Tasks;
using TheLizzards.Common.Data;

namespace TheLizzards.CQRS
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

		public async Task<IResult> Execute(ICommand command)
		{
			var castedCommand = (TCommand)command;

			return await this.Execute(castedCommand);
		}

		public void Dispose() => Dispose(true);

		protected virtual void DisposeResources()
		{
		}

		protected abstract Task<IResult> Execute(TCommand command);

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