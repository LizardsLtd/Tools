using System;

namespace TheLizzards.Data.Types.Contracts.Scoped
{
	[Obsolete]
	public interface IUserNameScoped<T> : IDisposable
	{
		T SetUserName(string username);
	}
}