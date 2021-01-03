using CommandLine;
using Tekook.VerbR.Contracts;

namespace Tests
{
    [Verb("json", isDefault: false)]
    internal class JsonOptions : ICanValidateOnly
    {
        [Option("config", Required = true)]
        public string Config { get; set; }

        /// <summary>
        /// Determinates if the config should only be validated and the verb should not be invoked.
        /// Usefull for checking config before starting a service.
        /// </summary>
        [Option("validation-only", Default = false, HelpText = "Only perform validation of configuration")]
        public bool ValidationOnly { get; set; }
    }
}