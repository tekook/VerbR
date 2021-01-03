using System.ComponentModel.DataAnnotations;

namespace Tests
{
    public interface MyConfig
    {
        Processor Processor { get; set; }

        [Required, MinLength(3)]
        string UserDomain { get; set; }

        string UserName { get; set; }
    }

    public interface Processor
    {
        public int Level { get; set; }
    }
}