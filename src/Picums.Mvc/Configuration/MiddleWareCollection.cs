using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace Picums.Mvc.Configuration
{
    public sealed class MiddlewareCollection
    {
        private readonly List<Action<IApplicationBuilder>> types;

        public MiddlewareCollection()
        {
            this.types = new List<Action<IApplicationBuilder>>();
        }

        internal void Use(IApplicationBuilder app)
        {
            this.types.ForEach(x => x(app));
        }

        internal void Add<T>() where T : class
        {
            this.types.Add(app => app.UseMiddleware<T>());
        }
    }
}