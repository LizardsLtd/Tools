using System;
using Picums.Data.CQRS;

namespace Picums.Data.CQRS.Commands
{
	public abstract class CommandBase : ICommand
	{
		public CommandBase()
		{
			this.CommandId = Guid.NewGuid();
		}

		public Guid CommandId { get; }
	}
}