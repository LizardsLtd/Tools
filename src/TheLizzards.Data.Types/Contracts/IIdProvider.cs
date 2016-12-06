using System;

namespace TheLizzards.Data.Types.Contracts
{
	public interface IIdProvider
	{
		Guid Id { get; }
	}
}