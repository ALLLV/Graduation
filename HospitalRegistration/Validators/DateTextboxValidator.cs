using System.Linq;
using System.Windows.Documents;
using System.Collections.Generic;
using System;

namespace HospitalRegistration.Validators
{
    public class DateTextboxValidator
    {
        static List<char> allowedChars = new List<char>
        {
            '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '.'
        };
        public static bool Validate(string text)
        {
            if (text.Any(x => !char.IsDigit(x)))
                return false;

            if (!text.Any(x => allowedChars.Contains(x)))
                return false;

            if (!DateTime.TryParse(text, out DateTime result))
                return false;

            return true;
        }
    }
}
