using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tekook.VerbR;

namespace Tests
{
    class Run : Verb<MyOptions, MyConfig>
    {
        public Run(MyOptions options) : base(options)
        {

        }

        public async override Task<int> InvokeAsync()
        {
            return 0;
        }
    }
}
