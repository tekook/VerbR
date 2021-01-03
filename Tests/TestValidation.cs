using Config.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tekook.VerbR;
using Tekook.VerbR.Contracts;
using Tekook.VerbR.Resolvers;
using Tekook.VerbR.Validators;

namespace Tests
{
    internal class TestValidation : Verb<ValidateOptions, MyConfig>
    {
        public TestValidation(ValidateOptions options) : base(options)
        {
            this.Resolver = new ConfigNetResolver<MyConfig, ValidateOptions>(builder => builder.UseJsonFile(options.Config));
            this.Validator = new DataAnnotationValidator();
        }

        public async override Task<int> InvokeAsync()
        {
            Console.WriteLine($"Hello {this.Config.UserName}, your ProcessorLevel is {this.Config.Processor.Level}!");
            return 0;
        }
    }

    internal class Val : DataAnnotationValidator
    {
        public new bool IsValid(object config, out IEnumerable<IValidationError> errors)
        {
            config = (MyConfig)config;
            return base.IsValid(config, out errors);
        }
    }
}