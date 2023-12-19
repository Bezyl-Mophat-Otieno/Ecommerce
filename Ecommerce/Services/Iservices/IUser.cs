using Ecommerce.Models;

namespace Ecommerce.Services.Iservices
{
    public interface IUser
    {
        Task<User> GetByEmail(string Email);
        Task<User> GetById(Guid Id);

        Task<string> RegisterUser(User user);

        Task<string> UpdateUserInfo();


        Task<string> DeleteUser(User user);


        Task<List<User>> GetUsers();

        Task<List<Object>> GetUserOrdersAndProducts(Guid Id);
    }
}
