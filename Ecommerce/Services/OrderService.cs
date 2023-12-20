using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services.Iservices;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class OrderService : IOrder
    {

        private readonly ApplicationDBContext _context;

        public OrderService(ApplicationDBContext context)
        {
            _context = context;

        }
        public async Task<string> CreateOrderAsync(Order order)
        {
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
                return "Order created successfully";



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Order Creation Failed";


            }
        }


        public async Task<string> DeleteOrderAsync(Order order)
        {

            try
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return "Order Deleted successfully";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Failed to Delete Order";


            }
        }

        public async Task<Order> GetOrderAsync(Guid Id)
        {

            try
            {
                var order = await _context.Orders.FindAsync(Id);
                return order;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;


            }
        }

        public async Task<List<Order>> GetOrdersAsync()
        {

            try
            {
                var orders = await _context.Orders.Include(x=>x.Products).ToListAsync();
                if (orders == null) return new List<Order>();
                return orders;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return new List<Order>();


            }
        }

        public async Task<string> UpdateOrderAsync()
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
