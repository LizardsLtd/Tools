using System;
using System.Collections.Generic;
using System.Text;

namespace TheLizzards.Search
{
	public interface IKeyWord
	{
		string[] SearchTokens { get; }

		string Combine();
	}
}
