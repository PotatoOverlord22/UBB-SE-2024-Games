﻿using System;

namespace GameWorldClassLibrary.exceptions
{
    public class NotYourTurnException : Exception
    {
        public NotYourTurnException()
        {
        }

        public NotYourTurnException(string message) : base(message)
        {
        }

        public NotYourTurnException(string message, Exception inner)
        {
        }
    }
}
