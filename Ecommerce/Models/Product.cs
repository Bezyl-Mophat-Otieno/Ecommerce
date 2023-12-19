using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ? Price { get; set; }


        public int Quantity { get; set; }

        public List<Order> orders { get; set; }
    }
}
