using System;

namespace TheLizzards.Data.CQRS.Contracts
{
	public interface ICommand
	{
		Guid CommandId { get; }
	}
}