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
        public static string ValidateRegexExtra(string trueString, string falseString, string pattern)
        {
            if (trueString == null) return falseString;

            return Regex.IsMatch(trueString, pattern) ? trueString : falseString;
        }
        public static bool ValidateQuantity(string quantityString)
        {
            if (!String.IsNullOrWhiteSpace(quantityString))
            {
                if (int.TryParse(quantityString, out int quantity) && int.Parse(quantityString) > 0)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
