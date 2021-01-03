namespace Tests
{
    public interface MyConfig
    {
        SubConfig JD2 { get; set; }
        string UserHome { get; set; }
        string UserName { get; set; }
    }

    public interface SubConfig
    {
        public string Home { get; set; }
    }
}