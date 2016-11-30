using System;

namespace TheLizzards.CQRS.Contracts
{
	public interface ICommand
	{
		Guid CommandId { get; }
	}
}