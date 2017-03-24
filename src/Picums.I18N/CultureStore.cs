using System;
using System.Collections.Generic;
using System.Globalization;

namespace Picums.I18N
{
    public sealed class CultureStore
    {
        /// <summary>Record Constructor</summary>
        /// <param name="defaultCulture"><see cref="DefaultCulture"/></param>
        /// <param name="availableCultures"><see cref="AvailableCultures"/></param>
        /// <param name="currentCulture"><see cref="CurrentCulture"/></param>
        public CultureStore(CultureInfo defaultCulture, IEnumerable<CultureInfo> availableCultures)
        {
            DefaultCulture = defaultCulture;
            AvailableCultures = availableCultures;
        }

        public CultureInfo DefaultCulture { get; }

        public IEnumerable<CultureInfo> AvailableCultures { get; }

        public CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

        public static implicit operator CultureStore(CultureInfo v)
        {
            throw new NotImplementedException();
        }
    }
}