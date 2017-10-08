using System;
using System.Collections.Generic;

namespace Picums.Mvc.Localisation
{
    internal sealed class CultureAndKeyComparer : IEqualityComparer<TranslationItem>
    {
        public bool Equals(TranslationItem x, TranslationItem y)
            => string.Equals(x.CultureName, y.CultureName, StringComparison.CurrentCultureIgnoreCase)
            && string.Equals(x.TranslationKey, y.TranslationKey, StringComparison.CurrentCultureIgnoreCase);

        public int GetHashCode(TranslationItem obj) => obj.GetHashCode();
    }
}