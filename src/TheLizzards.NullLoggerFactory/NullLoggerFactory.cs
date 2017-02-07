﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace TheLizzards.NullLoggerFactory
{
    public class NullLoggerFactory : ILoggerFactory
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
