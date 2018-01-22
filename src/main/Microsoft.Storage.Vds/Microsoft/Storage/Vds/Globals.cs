namespace Microsoft.Storage.Vds
{
    using System;

    public static class Globals
    {
        private static bool isMockVdsAvailable = true;

        public static bool IsMockObject(object value)
        {
            return ((value != null) && value.GetType().FullName.Contains("Microsoft.Storage.Vds.TestModel"));
        }

        public static bool IsMockVdsAvailable
        {
            get
            {
                return isMockVdsAvailable;
            }
            set
            {
                isMockVdsAvailable = value;
            }
        }
    }
}

