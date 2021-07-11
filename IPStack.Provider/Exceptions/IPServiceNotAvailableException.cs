using System;

namespace IPStack.Adapter.Exceptions
{
    public class IPServiceNotAvailableException: Exception
    {
        public IPServiceNotAvailableException()
        {
        }

        /// <summary>
        /// Constructor to be used when we have a general validation error.
        /// </summary>
        /// <param name="message">The error message we want to be returned.</param>
        public IPServiceNotAvailableException(string message)
            : base(message)
        {
        }

        public IPServiceNotAvailableException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
