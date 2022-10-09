using AutoMapper;
using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses.Departments;
using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Api.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<CreateDepartmentRequest, Department>();

            CreateMap<CreateOrderRequest, Order>();

            CreateMap<PaginationQueryRequest, PaginationQuery>();

            CreateMap<GetAllDepartmentsQuery, GetAllDepartmentsFilter>();
        }
    }
}
