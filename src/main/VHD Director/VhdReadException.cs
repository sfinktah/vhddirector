using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace VHD_Director
{
    /// <summary>
    /// A non-lethal exception to wrap read errors in faulty VHD files
    /// </summary>
    public class VhdReadException : Exception, ISerializable
    {
        private string message;
        private Exception innerException;

        public VhdReadException() : base()
        {
            // Add implementation.
        }
        public VhdReadException(string message) : base(message)
        {
            this.message = message;
        }

        public VhdReadException(string message, Exception inner) : base(message, inner)
        {
            this.message = message;
            this.innerException = inner;
        }

        // This constructor is needed for serialization.
        protected VhdReadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            // Add implementation.
        }
    }
}
