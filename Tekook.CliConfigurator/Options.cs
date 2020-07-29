using CommandLine;

namespace Tekook.CliConfigurator
{
    /// <summary>
    /// Base class for all options used via <see cref="CommandLine.Parser"/>.
    /// </summary>
    public abstract class Options
    {
        /// <summary>
        /// Path to the configuration file.
        /// </summary>
        [Option('c', "config", Required = false, HelpText = "Path to json config. If not supplied env will be used")]
        public string Config { get; set; }

        /// <summary>
        /// Determinates if the config should only be validated and the verb should not be invoked.
        /// Usefull for checking config before starting a service.
        /// </summary>
        [Option("validation-only", Default = false, HelpText = "Only perform validation of configuration")]
        public bool ValidationOnly { get; set; }
    }
}