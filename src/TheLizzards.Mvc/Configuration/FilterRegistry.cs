using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace TheLizzards.Mvc.Configuration
{
    public class FilterRegistry
    {
        private readonly List<Action<MvcOptions>> filters;

        internal FilterRegistry()
        {
            filters = new List<Action<MvcOptions>>(25);
        }

        public FilterRegistry Add<TFilterMetadata>() where TFilterMetadata : IFilterMetadata, new()
            => Add(new TFilterMetadata());

        public FilterRegistry Add(IFilterMetadata filter)
        {
            filters.Add(options => options.Filters.Add(filter));
            return this;
        }

        internal void Execute(MvcOptions options) => filters.ForEach(x => x(options));
    }
}