using System;

namespace Cinematheque.WebSite.Extensions
{
    public static class StringExtensions
    {
        public static bool Contains(this string source, string sub, StringComparison comp)
        {
            return source.IndexOf(sub, comp) >= 0;
        }
    }
}