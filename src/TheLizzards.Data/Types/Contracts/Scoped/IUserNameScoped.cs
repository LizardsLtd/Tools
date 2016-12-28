using System;

namespace TheLizzards.Data.Types.Contracts.Scoped
{
	public interface IUserNameScoped<T> : IDisposable
	{
		T SetUserName(string username);
	}
}