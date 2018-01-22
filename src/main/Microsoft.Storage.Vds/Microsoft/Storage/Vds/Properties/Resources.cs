namespace Microsoft.Storage.Vds.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [DebuggerNonUserCode, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0"), CompilerGenerated]
    public class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        public Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        public static string InvalidHardwareProviderIdArgumentExceptionMessage
        {
            get
            {
                return ResourceManager.GetString("InvalidHardwareProviderIdArgumentExceptionMessage", resourceCulture);
            }
        }

        public static string InvalidSoftwareProviderIdArgumentExceptionMessage
        {
            get
            {
                return ResourceManager.GetString("InvalidSoftwareProviderIdArgumentExceptionMessage", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("Microsoft.Storage.Vds.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        public static string UnableToGetLunNumberExceptionMessage
        {
            get
            {
                return ResourceManager.GetString("UnableToGetLunNumberExceptionMessage", resourceCulture);
            }
        }

        public static string UnknownObjectTypeInvalidOperationExceptionMessage
        {
            get
            {
                return ResourceManager.GetString("UnknownObjectTypeInvalidOperationExceptionMessage", resourceCulture);
            }
        }
    }
}

