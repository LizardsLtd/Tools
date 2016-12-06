using System.Collections.Generic;
using System.Linq;

namespace TheLizzards.Data.Azure.Entities
{
	public static class Materializer
	{
		public static IEnumerable<T> Materialize<T>(this IQueryable<T> items)
			=> items.ToArray();
	}
}