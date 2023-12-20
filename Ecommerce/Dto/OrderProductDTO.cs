using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dto
{
    public class OrderProductDTO
    {
        [Required]
        public Guid Id { get; set; }
    }
}
