﻿using System;

namespace IPStack.Adapter.Exceptions
{
    public class IPServiceNotAvailableException: Exception
    {
        public IPServiceNotAvailableException()
        {
        }

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
