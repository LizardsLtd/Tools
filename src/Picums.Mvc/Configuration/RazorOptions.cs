using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Picums.Mvc.Configuration
{
    public sealed class RazorOptions
    {
        private readonly List<Action<RazorViewEngineOptions>> actions;

        internal RazorOptions()
        {
            this.actions = new List<Action<RazorViewEngineOptions>>();
        }

        internal void Options(Action<RazorViewEngineOptions> options)
        {
            this.actions.Add(options);
        }

        internal void Use(IServiceCollection services)
            => services.Configure<RazorViewEngineOptions>(x => this.actions.ForEach(action => action(x)));
    }
}