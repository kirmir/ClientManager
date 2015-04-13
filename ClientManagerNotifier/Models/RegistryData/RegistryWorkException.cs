using System;

namespace ClientManagerNotifier.Models.RegistryData
{
    /// <summary>
    /// The exception used to registry work errors.
    /// </summary>
    public class RegistryWorkException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryWorkException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public RegistryWorkException(string message) : base(message)
        {
        }
    }
}