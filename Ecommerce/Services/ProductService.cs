using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services.Iservices;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class ProductService : IProduct
    {
        private readonly ApplicationDBContext _context;

        public ProductService(ApplicationDBContext context)
        {
            _context = context;
            
        }
        public async Task<string> AddProductAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return "Product added successfully";

                

            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Product add Failed";


            }
        }

        public async Task<string> DeleteProductsAsync(Product product)
        {

            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return "Product Deleted successfully";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Failed to Delete Product";


            }
        }

        public async Task<Product> GetProductAsync(Guid Id)
        {

            try
            {
                var product = await _context.Products.FindAsync(Id);
                return product;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;


            }
        }

        public async Task<List<Product>> GetProductsAsync()
        {

            try
            {
                var products = await _context.Products.ToListAsync();
                if(products == null) return new List<Product>();
                return products;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return new List<Product>();


            }
        }

        public async Task<string> UpdateProductAsync()
        {

            try
            {
                await _context.SaveChangesAsync();
                return "Update Successfull";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Update Failed";


            }
        }
    }
}
