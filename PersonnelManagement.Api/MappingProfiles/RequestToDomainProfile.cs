using AutoMapper;
using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Api.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<CreateDepartmentRequest, Department>();

            CreateMap<CreateOrderRequest, Order>();
        }
    }
}
