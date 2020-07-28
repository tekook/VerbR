using System;

namespace Tekook.CliConfigurator
{
    public class ConfigException : Exception
    {
        public string EnvName { get; protected set; }

        public string PropertyName { get; protected set; }

        public ConfigException(string propertyName, string envName, string message) : base(message)
        {
            this.PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            this.EnvName = envName ?? throw new ArgumentNullException(nameof(envName));
        }

        public ConfigException(string propertyName, string envName, string message, Exception innerException) : base(message, innerException)
        {
            this.PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            this.EnvName = envName ?? throw new ArgumentNullException(nameof(envName));
        }

        public ConfigException(string propertyName, string envName, Exception innerException) : base(innerException.Message, innerException)
        {
            this.PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
            this.EnvName = envName ?? throw new ArgumentNullException(nameof(envName));
        }

        public ConfigException(string message) : base(message)
        {
        }

        public ConfigException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}