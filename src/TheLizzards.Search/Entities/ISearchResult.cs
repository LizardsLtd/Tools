using System;
using System.Collections.Generic;
using System.Text;

namespace TheLizzards.Search.Entities
{
	public interface ISearchResult
	{
		double Score { get; }
	}
}
