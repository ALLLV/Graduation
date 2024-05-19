namespace HospitalRegistration.Validators
{
    public static class NullObjectsValidator
    {
        public static bool Validate(object checkedObject)
        {
            if (string.IsNullOrWhiteSpace(checkedObject as string))
                return true;

            if (checkedObject == null)
                return true;

            return false;
        }

        public static bool Validate(params object[] checkedObjects)
        {
            foreach (var checkedObject in checkedObjects)
            {
                if (checkedObject == null)
                    return true;

                if (string.IsNullOrWhiteSpace(checkedObject.ToString()))
                    return true;
            }

            return false;
        }
    }
}
