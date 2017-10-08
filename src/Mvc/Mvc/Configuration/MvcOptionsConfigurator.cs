using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Picums.Mvc.Configuration
{
    public sealed class MvcOptionsConfigurator
    {
        private readonly List<Action<MvcOptions>> actions;

        public MvcOptionsConfigurator()
        {
            this.actions = new List<Action<MvcOptions>>(25);
        }

        public MvcOptionsConfigurator Set(Action<MvcOptions> action)
        {
            this.actions.Add(action);
            return this;
        }

        internal void Execute(MvcOptions options)
            => this.actions.ForEach(x => x(options));
    }
}