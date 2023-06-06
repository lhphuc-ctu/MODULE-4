using System.ComponentModel.DataAnnotations;

namespace ConfigurationSample
{
    public class MyInfoOptions
    {
        public const string MyInfo = "MyInfo";
        public Address? Address { get; set; }
        [Range(0, int.MaxValue)]
        public int Age { get; set; }
    }

    public class Address
    {
        [Required]
        public string City { get; set; } = String.Empty;
        [Required]
        public string District { get; set; } = String.Empty;
    }
}
