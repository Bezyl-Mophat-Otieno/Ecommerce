using AutoMapper;
using Ecommerce.Dto;
using Ecommerce.Models;

namespace Ecommerce.Profiles
{
    public class EcommerceProfiles:Profile
    {
        public EcommerceProfiles()
        {
            CreateMap<AddProductDto,Product>().ReverseMap();
            CreateMap<CreateOrderDto,Order>().ReverseMap();
            CreateMap<RegisterUserDto,User>().ReverseMap();
            
        }
    }
}
