using System;

namespace Picums.Mvc.Navigation
{
	public interface IMenuAttribute : IEquatable<IMenuAttribute>
	{
		string Section { get; }

		string Name { get; }

		int OrderNumber { get; }

		int Level { get; }
	}
}