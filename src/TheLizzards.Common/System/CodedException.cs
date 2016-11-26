using System;

namespace TheLizzards.Common.System
{
	public abstract class CodedException : Exception
	{
		public CodedException()
			: this(string.Empty, null) { }

		public CodedException(string message)
			: this(message, null) { }

		public CodedException(string message, Exception innerException)
			: base(message, innerException) { }

		public int Code { get; protected set; }
	}
}