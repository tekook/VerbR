using Config.Net;
using System;
using System.Threading.Tasks;
using Tekook.VerbR;
using Tekook.VerbR.Resolvers;

namespace Tests
{
    internal class TestJson : Verb<JsonOptions, MyConfig>
    {
        public TestJson(JsonOptions options) : base(options)
        {
            this.Resolver = new ConfigNetResolver<MyConfig, JsonOptions>(builder => builder.UseJsonFile(options.Config));
        }

        public async override Task<int> InvokeAsync()
        {
            Console.WriteLine($"Hello {this.Config.UserName}, your ProcessorLevel is {this.Config.Processor.Level}!");
            return 0;
        }
    }
}