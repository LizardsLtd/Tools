using System;
using System.Collections.Generic;
using System.Linq;

namespace TheLizzards.Common.Data
{
	public sealed class Results : IResult
	{
		public static readonly Results Success = new Results();
		private List<string> messages;

		private Results()
		{
			messages = new List<string>();
		}

		private Results(params string[] messages) : this()
		{
			this.messages.AddRange(messages);
		}

		public bool IsSuccess => this.messages.Count == 0;

		public IEnumerable<string> ErrorMessages => this.messages.AsReadOnly();

		public static Results Fail(Exception exception)
			=> new Results(exception.Message);

		public static Results Fail(string message)
			=> new Results(message);

		public static IResult FromResults(params IResult[] results)
			=> new Results(results.SelectMany(x => x.ErrorMessages).ToArray());

		public Results AddFailure(string message)
					=> new Results(
				this
					.messages
					.Union(new[] { message })
					.ToArray());
	}
}