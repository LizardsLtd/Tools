using System;
using System.Collections.Generic;
using System.Linq;

namespace Picums.Maybe
{
	public static class MaybeEnumerableExtensions
	{
		public static Maybe<T> FirstOrNothing<T>(this IEnumerable<T> source)
		{
			var takeOne = source.Take(1).ToArray();

			return takeOne.Any() ? Maybe.From(takeOne.First()) : Maybe<T>.Nothing;
		}

		public static Maybe<T> FirstOrNothing<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			return source.Where(predicate).FirstOrNothing();
		}

		public static Maybe<T> LastOrNothing<T>(this IEnumerable<T> source)
		{
			var enumerable = source as T[] ?? source.ToArray();
			var lastOne = enumerable.Skip(Math.Max(0, enumerable.Length - 1)).ToList();

			return lastOne.Any() ? Maybe.From(lastOne.First()) : Maybe<T>.Nothing;
		}

		public static Maybe<T> LastOrNothing<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			return source.Where(predicate).LastOrNothing();
		}

		public static Maybe<T> SingleOrNothing<T>(this IEnumerable<T> source)
		{
			var takeTwo = source.Take(2).ToArray();

			return takeTwo.Length == 1 ? Maybe.From(takeTwo.First()) : Maybe<T>.Nothing;
		}

		public static Maybe<T> SingleOrNothing<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			return source.Where(predicate).SingleOrNothing();
		}

		public static IEnumerable<Maybe<T>> ToMaybeList<T>(this IEnumerable<T> source)
			=> source.Select(Maybe.From);
	}
}