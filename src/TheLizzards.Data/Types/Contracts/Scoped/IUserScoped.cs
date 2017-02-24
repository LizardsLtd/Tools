using System;

namespace TheLizzards.Data.Types.Contracts.Scoped
{
	[Obsolete]
	public interface IIdScoped<T> : IDisposable
	{
		T SetId(Guid id);
	}
}