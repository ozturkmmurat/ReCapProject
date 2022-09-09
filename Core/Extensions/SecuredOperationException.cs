using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Core.Extensions
{
    public class SecuredOperationException : Exception
    {
        public SecuredOperationException()
        {

        }
        public SecuredOperationException(string message) : base(message)
        {

        }
        public SecuredOperationException(string message, Exception innerException) : base(message, innerException)     
        {

        }

        protected SecuredOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
