using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TheLizzards.Data.CQRS.Contracts;

namespace TheLizzards.Data.CQRS.Entities
{
	public abstract class CommandHandlerBase<TCommand> : ICommandHandler
		where TCommand : ICommand
	{
		private bool disposedValue = false;

		protected CommandHandlerBase()
		{
		}

		public bool CanHandle(ICommand command)
			=> command is TCommand;

		public async Task Execute(ICommand command)
			=> await this.Execute((TCommand)command);

		public Task<IEnumerable<ValidationResult>> Validate(ICommand command)
			=> this.Validate((TCommand)command);

		public void Dispose() => Dispose(true);

		protected virtual void DisposeResources()
		{
		}

		protected abstract Task Execute(TCommand command);

		protected virtual Task<IEnumerable<ValidationResult>> Validate(TCommand commmand)
			=> Task.FromResult<IEnumerable<ValidationResult>>(new ValidationResult[0]);

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