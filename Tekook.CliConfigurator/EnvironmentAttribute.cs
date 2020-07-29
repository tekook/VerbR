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
    }
}