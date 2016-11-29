using System;
using System.Collections.Generic;

namespace TheLizzards.Maybe
{
	public struct Maybe<T> :
		IComparable<Maybe<T>>
		, IComparable<T>
		, IEquatable<Maybe<T>>
		, IEquatable<T>
	{
		/// <summary>
		/// Nothing value.
		/// </summary>
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

		public bool Equals(Maybe<T> other)
			=> this.CompareTo(other) == 0;

		public bool Equals(T other)
			=> this.CompareTo(other) == 0;

		//public static bool operator ==(Maybe<T> left, T right) => left.Equals(right);

		//public static bool operator !=(Maybe<T> left, T right) => !(left == right);

		//public static bool operator ==(Maybe<T> left, Maybe<T> right) => left.Equals(right);

		//public static bool operator !=(Maybe<T> left, Maybe<T> right) => !(left == right);

		//public static Maybe<T> From(T value) => value;

		//public T GetValueOrDefault()
		//	=> IsSome
		//		? Value
		//		: default(T);

		//bool IEquatable<Maybe<T>>.Equals(Maybe<T> other) => this.Equals(other.Value);

		//public override bool Equals(object obj) => GetHashCode().Equals(obj?.GetHashCode());

		//public override int GetHashCode() => this.Value?.GetHashCode() ?? 0;
	}
}