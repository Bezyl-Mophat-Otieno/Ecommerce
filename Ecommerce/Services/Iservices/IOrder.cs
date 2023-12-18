using Ecommerce.Models;

namespace Ecommerce.Services.Iservices
{
    public interface IOrder
    {
        Task<string>  UpdateOrderAsync();
        Task<List<Order>>GetOrdersAsync();

        Task<string> DeleteOrderAsync(Order order);

        Task<Order> GetOrderAsync(Guid id);

        Task<string> CreateOrderAsync(Order order);


    }
}
