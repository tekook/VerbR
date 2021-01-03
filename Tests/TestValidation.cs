using Config.Net;
using FluentValidation;
using System;
using System.Threading.Tasks;
using Tekook.VerbR;
using Tekook.VerbR.Resolvers;
using Tekook.VerbR.Validators;

namespace Tests
{
    internal class TestValidation : Verb<ValidateOptions, MyConfig>
    {
        public TestValidation(ValidateOptions options) : base(options)
        {
            this.Resolver = new ConfigNetResolver<MyConfig, ValidateOptions>(builder => builder.UseJsonFile(options.Config));
            this.Validator = new FluentValidator<MyConfig>((validator) =>
            {
                validator.RuleFor(x => x.UserDomain).NotNull().MinimumLength(3);
            });
            this.Validator = new DataAnnotationValidator();
        }

        public async override Task<int> InvokeAsync()
        {
            Console.WriteLine($"Hello {this.Config.UserName}, your ProcessorLevel is {this.Config.Processor.Level}!");
            return 0;
        }
    }
}