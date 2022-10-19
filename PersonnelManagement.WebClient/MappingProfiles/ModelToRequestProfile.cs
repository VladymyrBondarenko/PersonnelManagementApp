using AutoMapper;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using static PersonnelManagement.WebClient.Pages.Orders.Orders;

namespace PersonnelManagement.WebClient.MappingProfiles
{
    public class ModelToRequestProfile : Profile
    {
        public ModelToRequestProfile()
        {
            CreateMap<OrderModel, CreateOrderRequest>();

            CreateMap<OrderModel, UpdateOrderRequest>();
        }
    }
}
