using System.Runtime.Serialization;

namespace Application.Documents
{
    [Serializable]
    internal class InvalidLineException : Exception
    {
        public InvalidLineException()
        {
        }

        public InvalidLineException(string? message) : base(message)
        {
            
        }

        public InvalidLineException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidLineException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}