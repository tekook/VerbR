using Config.Net;
using System.Threading.Tasks;
using Tekook.VerbR;
using Tekook.VerbR.Resolvers;

namespace Tests
{
    internal class Run : Verb<MyOptions, MyConfig>
    {
        public Run(MyOptions options) : base(options)
        {
        }

        public async override Task<int> InvokeAsync()
        {
            return 0;
        }

        protected override void SetResolver()
        {
            this.Resolver = new ConfigNetResolver<MyConfig>((builder) => builder.UseEnvironmentVariables());
        }
    }
}