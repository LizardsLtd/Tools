using System;
using System.Collections.Generic;

namespace Picums.Maybe
{
	public struct Maybe<T> :
		IComparable<Maybe<T>>
		, IComparable<T>
		, IEquatable<Maybe<T>>
		, IEquatable<T>
	{
		public static readonly Maybe<T> Nothing = new Maybe<T>();

		private Maybe(T value)
		{
			if (value == null)
			{
				throw new NullReferenceException("Value provided for Maybe could not be null");
			}

			this.Value = value;
		}

		public T Value { get; }

		public bool IsSome => Value != null;

		public bool IsNone => !this.IsSome;

		public static implicit operator Maybe<T>(T value)
			=> value == null
				? Nothing
				: new Maybe<T>(value);

		public int CompareTo(Maybe<T> other)
			=> other.IsSome
				? CompareTo(other.Value)
				: 1;

		public int CompareTo(T other)
		{
			return IsNone
				? -1
				: Comparer<T>.Default.Compare(this.Value, other);
		}

		public bool Equals(Maybe<T> other) => this.CompareTo(other) == 0;

		public bool Equals(T other) => this.CompareTo(other) == 0;
	}
}