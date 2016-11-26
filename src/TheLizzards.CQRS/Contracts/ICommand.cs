namespace TheLizzards.CQRS
{
	using System;

	public interface ICommand
	{
		Guid CommandId { get; }
	}
}