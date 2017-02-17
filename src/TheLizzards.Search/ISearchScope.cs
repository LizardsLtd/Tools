using System;

namespace TheLizzards.Search
{
	public interface ISearchScope<T> : IDisposable
	{
		T SetSearchString(string searchFor);
	}
}