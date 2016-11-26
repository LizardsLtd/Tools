using System.Collections.Generic;

namespace TheLizzards.Common.Data
{
	public interface IResult
	{
		bool IsSuccess { get; }

		IEnumerable<string> ErrorMessages { get; }
	}
}