using AutoMapper;
using PersonnelManagement.Contracts.v1.Requests.Identity;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.WebClient.Models;
using static PersonnelManagement.WebClient.Pages.Orders.Orders;

namespace PersonnelManagement.WebClient.MappingProfiles
{
    public class ModelToRequestProfile : Profile
    {
        public ModelToRequestProfile()
        {
            CreateMap<OrderModel, CreateOrderRequest>();

            CreateMap<OrderModel, UpdateOrderRequest>();

            CreateMap<RegisterModel, UserRegistrationRequest>();

            CreateMap<LoginModel, UserLoginRequest>();
        }
    }
}
