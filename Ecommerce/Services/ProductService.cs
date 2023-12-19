using AutoMapper.QueryableExtensions;
using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services.Iservices;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
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

        public async Task<List<Product>> GetProductsAsync(int size , int page)
        {

            try
            {
                var products = _context.Products.AsQueryable<Product>();

                if (size>0 && page>0)
                {
                products = products.Skip((size * (page - 1))).Take(size);

                    return await products.ToListAsync();


                }

                if(products == null) return new List<Product>();
                

                return products.ToList();

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
