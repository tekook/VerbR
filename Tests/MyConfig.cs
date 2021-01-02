using System;
using Tekook.VerbR.Scaffold;

namespace Tests
{
    internal class MyConfig : Config
    {
        public PrefixConfig Prefix { get; set; }

        public string Profile { get; set; }

        public SubConfig Sub { get; set; }

        internal class PrefixConfig
        {
            public string Domain { get; set; }
        }

        internal class SubConfig
        {
            public string UserName { get; set; }
        }
    }
}