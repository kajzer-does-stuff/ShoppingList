using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShoppingList.Models
{
    public class UtilityThings
    {
        public static string checkRegex(string trueString, string falseString, string pattern)
        {
            if (Regex.IsMatch(trueString, pattern))
            {
                return trueString;
            }
            else
            {
                return falseString;
            }
        }
    }
}
