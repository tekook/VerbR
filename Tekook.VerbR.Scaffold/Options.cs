using CommandLine;
using Tekook.VerbR.Contracts;

namespace Tekook.VerbR.Scaffold
{
    /// <summary>
    /// Implementation of options containing config
    /// </summary>
    public class Options : IConfigOptions
    {
        /// <summary>
        /// Path to the configuration file.
        /// </summary>
        [Option('c', "config", Required = false, HelpText = "Path to config file.")]
        public string Config { get; set; }

        /// <summary>
        /// Determinates if the config should only be validated and the verb should not be invoked.
        /// Usefull for checking config before starting a service.
        /// </summary>
        [Option("validation-only", Default = false, HelpText = "Only perform validation of configuration")]
        public bool ValidationOnly { get; set; }
    }
}