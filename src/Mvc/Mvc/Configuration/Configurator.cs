using System;
using System.Collections.Generic;

namespace Picums.Mvc.Configuration
{
    public sealed class Configurator<TAction>
    {
        private readonly List<Action<TAction>> templates;

        internal Configurator()
        {
            this.templates = new List<Action<TAction>>();
        }

        public Configurator<TAction> Add(Action<TAction> action)
        {
            this.templates.Add(action);
            return this;
        }

        internal void Execute(TAction actionAttribute)
            => this.templates.ForEach(x => x(actionAttribute));
    }
}