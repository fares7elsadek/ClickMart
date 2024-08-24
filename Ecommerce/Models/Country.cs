namespace Ecommerce.Models
{
    public class Country
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ISO { get; set; }
        public List<Address> Address { get; set; } = new List<Address>();
    }
}
