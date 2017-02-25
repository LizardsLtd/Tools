﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace TheLizzards.Tests
{
	public class TestLoggerFactory : ILoggerFactory
	{
		public void AddProvider(ILoggerProvider provider)
		{
		}

		public ILogger CreateLogger(string categoryName)
			=> NullLogger.Instance;

		public void Dispose()
		{
		}
	}
}