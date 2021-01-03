using Config.Net;
using System;
using System.Threading.Tasks;
using Tekook.VerbR;
using Tekook.VerbR.Resolvers;

namespace Tests
{
    internal class TestEnv : Verb<EnvOptions, MyConfig>
    {
        public TestEnv(EnvOptions options) : base(options)
        {
            this.Resolver = new ConfigNetResolver<MyConfig, EnvOptions>((builder) => builder.UseEnvironmentVariables());
        }

        public async override Task<int> InvokeAsync()
        {
            Console.WriteLine($"Hello {this.Config.UserName}, your ProcessorLevel is {this.Config.Processor.Level}!");
            return 0;
        }
    }
}