using System;

namespace TheLizzards.DataParts.Contracts
{
	public interface IIdProvider
	{
		Guid Id { get; }
	}
}