using Ecommerce.Models;

namespace Ecommerce.Dto
{
    public class PaginationMetadataDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<Product> products { get; set; } = default!;
    }
}
