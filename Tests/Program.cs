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
            Parser.Default.ParseArguments<MyOptions>(args)
                .WithParsed(Run)
                .WithNotParsed(HandleParseError);
        }

        private static void Run(MyOptions options)
        {
            (new Run(options)).Invoke();
        }
    }
}