using System;

namespace TheLizzards.Data.CQRS.Contracts
{
	public interface IsQuery : IDisposable
	{
	}

	public interface IQuery<out TPayload> : IsQuery
	{
		TPayload Execute();
	}
}