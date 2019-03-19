using System;

namespace DesignPatterns.Examples.Extensions
{
    public static class StringExtensions
    {
        public static bool EqualsIC(this String string1, string string2)
        {
            return string1.Equals(string2, StringComparison.OrdinalIgnoreCase);
        }
    }
}
