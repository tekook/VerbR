using System;
using Tekook.VerbR;

namespace Tests
{
    internal class MyConfig : Config
    {
        [EnvironmentObject(Prefix = "USER")]
        public PrefixConfig Prefix { get; set; }

        [Environment(Name = "USERPROFILE")]
        public string Profile { get; set; }

        [EnvironmentObject]
        public SubConfig Sub { get; set; }

        internal class PrefixConfig
        {
            [Environment("DOMAIN")]
            public string Domain { get; set; }
        }

        internal class SubConfig
        {
            [Environment("USERNAME")]
            public string UserName { get; set; }
        }
    }
}