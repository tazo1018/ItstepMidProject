using System.Runtime.Serialization;

namespace ATMproject
{
    [Serializable]
    internal class IncompatibleCurrencyException : Exception
    {
        public IncompatibleCurrencyException()
        {
        }

        public IncompatibleCurrencyException(string? message) : base(message)
        {
        }

        public IncompatibleCurrencyException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected IncompatibleCurrencyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}