using System;

namespace TheLizzards.CQRS.Scoped
{
	public interface IIdScoped<T> : IDisposable
	{
		T SetId(Guid id);
	}
}