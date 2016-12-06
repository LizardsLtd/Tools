using System;
using TheLizzards.Data.CQRS.Contracts;

namespace TheLizzards.CQRS.Entities
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