using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace Picums.Mvc.Configuration
{
    public class FilterRegistry
    {
        private readonly List<Action<Microsoft.AspNetCore.Mvc.MvcOptions>> filters;

        internal FilterRegistry()
        {
            filters = new List<Action<Microsoft.AspNetCore.Mvc.MvcOptions>>(25);
        }

        public FilterRegistry Add<TFilterMetadata>() where TFilterMetadata : IFilterMetadata, new()
            => Add(new TFilterMetadata());

        public FilterRegistry Add(IFilterMetadata filter)
        {
            filters.Add(options => options.Filters.Add(filter));
            return this;
        }

        internal void Execute(Microsoft.AspNetCore.Mvc.MvcOptions options) => filters.ForEach(x => x(options));

        internal void Add(object configureFilter)
        {
            throw new NotImplementedException();
        }
    }
}