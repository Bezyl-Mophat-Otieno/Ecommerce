using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services.Iservices;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Services
{
    public class Userservice:IUser
    {
        private readonly ApplicationDBContext _context;

        public Userservice(ApplicationDBContext context)

        {

            _context = context;

        }

        public async Task<string> DeleteUser(User user)
        {
            try
            {

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return "User deleted successfully";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "User deleted successfully";

            }
        }

        public async Task<User> GetByEmail(string Email)
        {
            try
            {

                var user = await _context.Users.Where(x => x.Email.ToLower() == Email.ToLower()).FirstOrDefaultAsync();

                if (user != null)
                {
                    return user;
                }

                return null;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }

        public async Task<User> GetById(Guid Id)
        {
            try
            {

                var user = await _context.Users.FindAsync(Id);
                return user;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;

            }
        }

        public async Task<List<object>> GetUserOrdersAndProducts(Guid  Id)
        {
            try {

                var OrdersWithProducts = await _context.Orders.Where(o => o.UserId == Id).Include(o=>o.Products).ToListAsync<Object>();

               if(OrdersWithProducts.Count > 0)
                {
                    return OrdersWithProducts;
                }
               return new List<Object>();


            } catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return new List<Object>();

            }
        }

        public async Task<List<User>> GetUsers()
        {
            try
            {

                var users = await _context.Users.ToListAsync();
                return users;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<User>();

            }
        }

        public async Task<string> RegisterUser(User user)
        {
            try
            {

                await _context.Users.AddAsync(user);

                await _context.SaveChangesAsync();

                return "User added successfully";


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return "Something went wrong User not Registered";
            }
        }

        public async Task<string> UpdateUserInfo()
        {
            try
            {

                await _context.SaveChangesAsync();
                return "User information updated successfully";

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "User Updated SUccessfully";


            }
        }
    }
}
