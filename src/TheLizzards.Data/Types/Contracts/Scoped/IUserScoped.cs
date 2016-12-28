using System;

namespace TheLizzards.Data.Types.Contracts.Scoped
{
	public interface IIdScoped<T> : IDisposable
	{
		T SetId(Guid id);
	}
}