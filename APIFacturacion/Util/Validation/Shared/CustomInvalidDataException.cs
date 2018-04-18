using System;
using System.Runtime.Serialization;

namespace APIFacturacion.Util.Validation.Shared
{
    [Serializable]
    internal class CustomInvalidDataException : Exception
    {
        public CustomInvalidDataException()
        {
        }

        public CustomInvalidDataException(string message) : base(message)
        {
        }

        public CustomInvalidDataException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomInvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}