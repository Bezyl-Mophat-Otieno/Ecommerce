using Ecommerce.Models;

namespace Ecommerce.Services.Iservices
{
    public interface IJwt
    {
        string GetToken(User user);
    }
}
