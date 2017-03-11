using System;
using System.Collections.Generic;

namespace TheLizzards.Mvc.Localisation
{
    internal sealed class CultureAndKeyComparer : IEqualityComparer<(string, string, string)>
    {
        public bool Equals((string, string, string) x, (string, string, string) y)
            => string.Equals(x.Item1, y.Item1, StringComparison.CurrentCultureIgnoreCase)
            && string.Equals(x.Item2, y.Item2, StringComparison.CurrentCultureIgnoreCase);

        public int GetHashCode((string, string, string) obj)
            => $"{obj.Item1}:{obj.Item2}".GetHashCode();
    }
}