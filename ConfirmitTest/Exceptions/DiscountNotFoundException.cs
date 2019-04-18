using System;
using System.Runtime.Serialization;

namespace ConfirmitTest.Exceptions
{
    [Serializable]
    public class DiscountNotFoundException : ApplicationException
    {
        public DiscountNotFoundException(string message) : base(message)
        {
        }

        protected DiscountNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}