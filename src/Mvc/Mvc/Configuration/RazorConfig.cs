using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace Picums.Mvc.Configuration
{
    public sealed class RazorConfig
    {
        private readonly List<Action<RazorViewEngineOptions>> actions;

        internal RazorConfig()
            => this.actions = new List<Action<RazorViewEngineOptions>>();

        internal void Options(Action<RazorViewEngineOptions> options)
            => this.actions.Add(options);

        internal void Use(IServiceCollection services)
            => services.Configure<RazorViewEngineOptions>(x => this.actions.ForEach(action => action(x)));
    }
}