using System;

namespace TheLizzards.CQRS
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