using CommandLine;

namespace Tekook.CliConfigurator
{
    public abstract class Options
    {
        [Option('c', "config", Required = false, HelpText = "Path to json config. If not supplied env will be used")]
        public string Config { get; set; }

        [Option("validation-only", Default = false, HelpText = "Only perform validation of configuration")]
        public bool ValidationOnly { get; set; }
    }
}