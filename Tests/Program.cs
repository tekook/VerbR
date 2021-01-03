using CommandLine;
using System;
using System.Collections.Generic;

namespace Tests
{
    internal class Program
    {
        private static void HandleParseError(IEnumerable<Error> obj)
        {
            obj.Output();
            if (obj.IsHelp() || obj.IsVersion())
            {
                Environment.Exit(0);
            }
            else
            {
                Environment.Exit(1);
            }
        }

        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<EnvOptions, JsonOptions>(args)
                .WithParsed<EnvOptions>(o => (new TestEnv(o)).Invoke())
                .WithParsed<JsonOptions>(o => (new TestJson(o)).Invoke())
                .WithNotParsed(HandleParseError);
        }
    }
}