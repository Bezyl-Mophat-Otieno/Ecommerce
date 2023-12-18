using Ecommerce.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Dto
{
    public class CreateOrderDto
    {
        public DateTime OrderDate { get; set; } = new DateTime();

    }
}
