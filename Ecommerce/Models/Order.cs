using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime OrderDate { get; set; } = new DateTime();


        public List<Product> Products { get; set; } = default!;

        [ForeignKey("UserId")]
        public User User { get; set; } = default!;


        public Guid UserId { get; set; }


    }
}
