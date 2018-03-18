using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;

namespace Picums.Mvc.Localisation
{
    public sealed class CultureStore
    {
        public CultureStore(IConfiguration configuration)
            : this(
                GetDefaultLanguage(configuration),
                GetAvailableLanguages(configuration))
        { }

        /// <summary>Record Constructor</summary>
        /// <param name="defaultCulture"><see cref="DefaultCulture"/></param>
        /// <param name="availableCultures"><see cref="AvailableCultures"/></param>
        /// <param name="currentCulture"><see cref="CurrentCulture"/></param>
        public CultureStore(CultureInfo defaultCulture, IEnumerable<CultureInfo> availableCultures)
        {
            this.DefaultCulture = defaultCulture;
            this.AvailableCultures = availableCultures.ToList();
        }

        public CultureInfo DefaultCulture { get; }

        public List<CultureInfo> AvailableCultures { get; }

        public CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

        public RequestCulture RequestCulture => new RequestCulture(this.DefaultCulture);

        private static CultureInfo GetDefaultLanguage(IConfiguration configuration)
            => new CultureInfo(configuration["Culture:Default"]);

        private static IEnumerable<CultureInfo> GetAvailableLanguages(IConfiguration configuration)
            => configuration
                .GetSection("Culture:Available")
                .GetChildren()
                .Select(x => x.Value)
                .Select(x => new CultureInfo(x))
                .ToList();
    }
}