using System;

namespace TheLizzards.Data.CQRS
{
	public interface ICommand
	{
		Guid CommandId { get; }
	}
}