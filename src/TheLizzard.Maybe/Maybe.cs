using System;

namespace TheLizzards.Maybe
{
	public struct Maybe<T> : IEquatable<Maybe<T>>
	{
		/// <summary>
		/// Nothing value.
		/// </summary>
		public static readonly Maybe<T> Nothing = new Maybe<T>();

		private Maybe(T value)
		{
			if (value == null)
			{
				throw new InvalidOperationException("value is not present");
			}

			this.Value = value;
		}

		public T Value { get; }

		public bool HasValue => Value != null;

		public bool HasNoValue => !this.HasValue;

		public static implicit operator Maybe<T>(T value)
			=> value == null
				? Nothing
				: new Maybe<T>(value);

		public static bool operator ==(Maybe<T> left, T right) => left.Equals(right);

		public static bool operator !=(Maybe<T> left, T right) => !(left == right);

		public static bool operator ==(Maybe<T> left, Maybe<T> right) => left.Equals(right);

		public static bool operator !=(Maybe<T> left, Maybe<T> right) => !(left == right);

		public T GetValueOrDefault()
			=> HasValue
				? Value
				: default(T);

		bool IEquatable<Maybe<T>>.Equals(Maybe<T> other) => this.Equals(other.Value);

		public override bool Equals(object obj) => GetHashCode().Equals(obj?.GetHashCode());

		public override int GetHashCode() => this.Value?.GetHashCode() ?? 0;
	}
}