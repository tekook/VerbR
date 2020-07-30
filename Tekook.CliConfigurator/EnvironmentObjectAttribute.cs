using System;

namespace Tekook.CliConfigurator
{
    /// <summary>
    /// Declares a property which should be recursivly parsed with env vars
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class EnvironmentObjectAttribute : Attribute
    {
        /// <summary>
        /// Prefix of all environment variables to use for this object.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="prefix"><see cref="Prefix"/></param>
        public EnvironmentObjectAttribute(string prefix)
        {
            this.Prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public EnvironmentObjectAttribute()
        {
        }
    }
}