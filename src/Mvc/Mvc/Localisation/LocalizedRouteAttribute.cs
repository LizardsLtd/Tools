﻿using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace Picums.Mvc.Localisation
{
    public sealed class LocalizedRouteAttribute : RouteAttribute
    {
        public LocalizedRouteAttribute(string template, string culture) : base(template)
        {
            Culture = culture;
        }

        public LocalizedRouteAttribute(string template) : base(template)
        {
        }

        public string Culture { get; set; }

        public CultureInfo ToCultureInfo() => new CultureInfo(this.Culture);
    }
}