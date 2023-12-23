using System.Globalization;

namespace Lab2
{
    public class Comparator
    {
        public static bool IgnoreCaseCompare(string first, string second)
        {
            return string.Compare(first, second, true, CultureInfo.CurrentCulture) == 0;
        }
    }
}