using System;
using TheLizzards.Data.Types.Entites;

namespace TheLizzards.Data.Types.Contracts.Scoped
{
	public interface IEmailScoped<T> : IDisposable
	{
		T SetEmail(Email email);
	}
}