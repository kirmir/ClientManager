﻿using System;

namespace ClientManagerNotifier.Models.DataTransfer
{
    /// <summary>
    /// The exception show's when it's problems with connection to MVC service.
    /// </summary>
    public class DataTransferConnectionProblemException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Exception"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception. </param><param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified. </param>
        public DataTransferConnectionProblemException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}