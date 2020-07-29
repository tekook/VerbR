using System;

namespace Tekook.CliConfigurator
{
    /// <summary>
    /// Attribute for Properties to map an env value.
    /// </summary>

    public class EnvironmentAttribute : Attribute
    {
        /// <summary>
        /// Name of the env variable.
        /// </summary>
        public string Name;

        /// <summary>
        /// Creates a new Instance.
        /// </summary>
        public EnvironmentAttribute()
        {
        }

        /// <summary>
        /// Creates a new Instance.
        /// </summary>
        /// <param name="name">Name of the env variable.</param>
        public EnvironmentAttribute(string name) : this()
        {
            this.Name = name;
        }
    }
}