using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Dto
{
    public class CreateOrderDto
    {
        [Required]
        public DateTime OrderDate { get; set; } = new DateTime();

        [Required]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [Required]
        public List<Product> Products { get; set; } = default!;






    }
}
