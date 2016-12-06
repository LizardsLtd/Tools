using System;

namespace TheLizzards.CQRS.Scoped
{
	public interface IUserNameScoped<T> : IDisposable
	{
		T SetUserName(string username);
	}
}