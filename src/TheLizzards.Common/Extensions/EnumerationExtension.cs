using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheLizzards.Common.Extensions
{
	public static class EnumerationExtension
	{
		public static void ExecuteAction<T>(
			this IEnumerable<T> collection
			, Action<T> actionToPerform)
		{
			foreach (var item in collection)
			{
				actionToPerform(item);
			}
		}

		public static async Task ExecuteActionAsync<T>(
		   this IEnumerable<T> collection
		   , Func<T, Task> actionToPerform)
		{
			foreach (var item in collection)
			{
				await actionToPerform(item);
			}
		}
	}
}