using System;

namespace Picums.Mvc.Navigation
{
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	public sealed class MenuAttribute : Attribute, IMenuAttribute
	{
		public MenuAttribute(
			string section
			, string name
			, int orderNumber
			, int level = 1)
		{
			this.Section = section;
			this.Name = name;
			this.OrderNumber = orderNumber;
			this.Level = level;
		}

		public string Section { get; }

		public string Name { get; }

		public int OrderNumber { get; }

		public int Level { get; }

		public bool Equals(IMenuAttribute right)
			=> this.GetHashCode() == right?.GetHashCode();

		public override bool Equals(object obj)
			=> this.Equals(obj as IMenuAttribute);

		public override int GetHashCode()
			=> $"{Section}{Name}{OrderNumber}{Level}".GetHashCode();
	}
}