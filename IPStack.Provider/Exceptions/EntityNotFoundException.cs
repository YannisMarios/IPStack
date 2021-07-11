using System;
using System.Collections.Generic;
using System.Text;

namespace IPStack.Adapter.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() { }

        /// <summary>
        /// Constructor to be used when we have a general validation error.
        /// </summary>
        /// <param name="message">The error message we want to be returned.</param>
        public EntityNotFoundException(string message) : base(message) { }


        public EntityNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
