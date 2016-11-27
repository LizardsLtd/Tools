using System;

namespace TheLizzards.CQRS
{
	public interface IsQuery : IDisposable
	{
	}

	public interface IQuery<out TPayload> : IsQuery
	{
		TPayload Execute();
	}
}