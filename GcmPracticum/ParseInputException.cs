using System;

namespace GcmPracticum
{
    public class ParseInputException : Exception
    {
        public ParseInputException(string message) : base(message)
        {
        }

        public ParseInputException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
