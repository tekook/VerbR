namespace Tests
{
    internal interface MyConfig
    {
        public SubConfig JD2 { get; set; }
        public string UserHome { get; set; }
        public string UserName { get; set; }

        internal class SubConfig
        {
            public string Home { get; set; }
        }
    }
}