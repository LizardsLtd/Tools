using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLizzards.Maybe
{
	public sealed class Maybe
	{
		public static Maybe<T> None<T>() => Maybe<T>.Nothing;

		public static Maybe<T> From<T>(T item) => (Maybe<T>)item;
	}
}