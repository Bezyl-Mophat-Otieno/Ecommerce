using Ecommerce.Dto;
using Ecommerce.Models;

namespace Ecommerce.Services.Iservices
{
    public interface IProduct
    {

        Task<string> UpdateProductAsync();
        Task<PaginationMetadataDto> GetProductsAsync(int size , int page);

        Task<string> DeleteProductsAsync(Product product);

        Task<Product> GetProductAsync(Guid Id);

        Task<string> AddProductAsync(Product product);

    }
}
