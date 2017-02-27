using System;
using TheLizzards.Data.CQRS;

namespace TheLizzards.Data.CQRS.Commands
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