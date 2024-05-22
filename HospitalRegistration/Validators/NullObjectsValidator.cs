namespace HospitalRegistration.Validators
{
    public static class NullObjectsValidator
    {
        /// <summary>
        /// Returns true if given object is not null
        /// </summary>
        public static bool Validate(object checkedObject)
        {
            if (string.IsNullOrWhiteSpace(checkedObject as string))
                return false;

            if (checkedObject == null)
                return false;

            return true;
        }

        /// <summary>
        /// Returns true if given objects are not null
        /// </summary>
        public static bool Validate(params object[] checkedObjects)
        {
            foreach (var checkedObject in checkedObjects)
            {
                if (checkedObject == null)
                    return false;

                if (string.IsNullOrWhiteSpace(checkedObject.ToString()))
                    return false;
            }

            return true;
        }
    }
}
