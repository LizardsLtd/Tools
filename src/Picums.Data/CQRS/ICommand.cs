using System;

namespace Picums.Data.CQRS
{
	public interface ICommand
	{
		Guid CommandId { get; }
	}
}