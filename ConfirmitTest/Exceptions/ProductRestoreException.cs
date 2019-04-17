using System;
using System.Runtime.Serialization;

namespace ConfirmitTest.Exceptions
{
    [Serializable]
    public class ProductRestoreException : ApplicationException
    {
        public ProductRestoreException()
        {
        }

        public ProductRestoreException(string message) : base(message)
        {
        }

        public ProductRestoreException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProductRestoreException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}