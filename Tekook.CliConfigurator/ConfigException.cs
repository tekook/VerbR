using System;

namespace Tekook.CliConfigurator
{
    /// <summary>
    /// Exception for all configuratoin errors.
    /// </summary>
    public class ConfigException : Exception
    {
        /// <summary>
        /// Env which triggered the Exception.
        /// Null if <see cref="EnvSetterAttribute"/> was not the reason.
        /// </summary>
        public string EnvName { get; protected set; }

        /// <summary>
        /// Property which triggered the Exception.
        /// Null if <see cref="EnvSetterAttribute"/> was not the reason.
        /// </summary>
        public string PropertyName { get; protected set; }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="propertyName">Property which caused the exception.</param>
        /// <param name="envName">Env which caused the exception.</param>
        /// <param name="message">Message of the exeption.</param>
        public ConfigException(string propertyName, string envName, string message) : base(message)
        {
            this.PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            this.EnvName = envName ?? throw new ArgumentNullException(nameof(envName));
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="propertyName">Property which caused the exception.</param>
        /// <param name="envName">Env which caused the exception.</param>
        /// <param name="message">Message of the exeption.</param>
        /// <param name="innerException">InnerException which caused this exception.</param>
        public ConfigException(string propertyName, string envName, string message, Exception innerException) : base(message, innerException)
        {
            this.PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            this.EnvName = envName ?? throw new ArgumentNullException(nameof(envName));
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <param name="propertyName">Property which caused the exception.</param>
        /// <param name="envName">Env which caused the exception.</param>
        /// <param name="innerException">InnerException which caused this exception. Message of InnerException will be used for message attribute.</param>
        public ConfigException(string propertyName, string envName, Exception innerException) : base(innerException.Message, innerException)
        {
            this.PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            this.EnvName = envName ?? throw new ArgumentNullException(nameof(envName));
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        public ConfigException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="message">Message of the exception.</param>
        /// <param name="innerException">InnerException which caused this exception.</param>
        public ConfigException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}