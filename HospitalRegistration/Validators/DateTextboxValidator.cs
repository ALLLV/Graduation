using System;

namespace HospitalRegistration.Validators
{
    public class DateTextboxValidator
    {
        public static bool Validate(string text)
        {
            if (!DateTime.TryParse(text, out DateTime result))
                return false;

            return true;
        }
    }
}
