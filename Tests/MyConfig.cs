namespace Tests
{
    public interface MyConfig
    {
        Processor Processor { get; set; }
        string UserName { get; set; }
    }

    public interface Processor
    {
        public int Level { get; set; }
    }
}