using System;
using System.Runtime.Serialization;

namespace ConfirmitTest.Exceptions
{
    [Serializable]
    public class DiscountNotFoundException : DiscountException
    {
        public DiscountNotFoundException()
        {
        }

        public DiscountNotFoundException(string message) : base(message)
        {
        }

        public DiscountNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DiscountNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}