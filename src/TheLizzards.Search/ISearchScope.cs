using System;
using System.Collections.Generic;
using System.Text;

namespace TheLizzards.Search
{
	public interface ISearchScope<T> : IDisposable
	{
		T SetSearchString(string searchFor);
	}
}
