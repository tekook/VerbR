using CommandLine;
using Tekook.VerbR.Contracts;

namespace Tekook.VerbR.Scaffold
{
    /// <summary>
    /// Implementation of options containing config
    /// </summary>
    public class Options : ICanValidateOnly
    {
        /// <summary>
        /// Determinates if the config should only be validated and the verb should not be invoked.
        /// Usefull for checking config before starting a service.
        /// </summary>
        [Option("validation-only", Default = false, HelpText = "Only perform validation of configuration")]
        public bool ValidationOnly { get; set; }
    }
}