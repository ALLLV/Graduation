using System.Linq;
using System.Windows.Input;

namespace HospitalRegistration.Validators
{
    public static class DigitTextboxValidator
    {
        public static bool Validate(string text)
        {
            if (text.Any(x => !char.IsDigit(x)))
                return false;

            if (text != System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.DateSeparator)
                return false;
            
            return true;
        }

        public static bool Validate(Key key)
        {
            if (key < Key.D0 || key > Key.D9)
                return false;
            
            return true;
        }
    }
}
