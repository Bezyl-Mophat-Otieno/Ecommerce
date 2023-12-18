using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;


        public string Email { get; set; } = string.Empty;


        public string Password { get; set; } = string.Empty;

        public string Role {  get; set; } = "User";

        public List<Order> Orders { get; set; } = default!;
    }
}
