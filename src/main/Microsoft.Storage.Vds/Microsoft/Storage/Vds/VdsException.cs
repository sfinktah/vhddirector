namespace Microsoft.Storage.Vds
{
    using System;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;

    [Serializable]
    public class VdsException : Exception
    {
        private string interfaceName;
        private string methodName;

        public VdsException()
        {
        }

        public VdsException(string message) : base(message)
        {
        }

        protected VdsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public VdsException(string message, Exception inner) : base(message, inner)
        {
        }

        public VdsException(string message, string interfaceName, string methodName, uint hresult) : base(message)
        {
            this.interfaceName = interfaceName;
            this.methodName = methodName;
        }

        public static bool IsFatalException(Exception exception)
        {
            return ((exception != null) && ((exception is NullReferenceException) || (exception is SEHException)));
        }

        public override string ToString()
        {
            return (this.Message + " : " + base.InnerException);
        }

        public string InterfaceName
        {
            get
            {
                return this.interfaceName;
            }
            set
            {
                this.interfaceName = value;
            }
        }

        public string MethodName
        {
            get
            {
                return this.methodName;
            }
            set
            {
                this.methodName = value;
            }
        }
    }
}

