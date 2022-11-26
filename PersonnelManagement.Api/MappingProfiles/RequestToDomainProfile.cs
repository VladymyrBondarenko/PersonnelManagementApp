using AutoMapper;
using PersonnelManagement.Contracts.v1.Requests;
using PersonnelManagement.Contracts.v1.Requests.Employees;
using PersonnelManagement.Contracts.v1.Requests.Identity;
using PersonnelManagement.Contracts.v1.Requests.Orders;
using PersonnelManagement.Contracts.v1.Requests.Positions;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses.Departments;
using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Models.Identity;
using PersonnelManagement.Domain.Orders;
using PersonnelManagement.Domain.Positions;

namespace PersonnelManagement.Api.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            // departments
            CreateMap<CreateDepartmentRequest, Department>();
            CreateMap<GetAllDepartmentsQuery, GetAllDepartmentsFilter>();

            // positions
            CreateMap<CreatePositionRequest, Position>();
            CreateMap<GetAllPositionsQuery, GetAllPositionsFilter>();

            // orders
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<GetAllOrdersQuery, GetAllOrdersFilter>();
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<GetAllOrderDescriptionsQuery, GetAllOrderDescriptionsFilter>();
            CreateMap<CreateOrderDescriptionRequest, OrderDescription>();

            //employees
            CreateMap<GetAllEmployeesQuery, GetAllEmployeesFilter>();
            CreateMap<CreateEmployeeRequest, Employee>();

            //originals
            CreateMap<GetAllOriginalsQuery, GetAllOriginalsFilter>();

            //identity
            CreateMap<UserRegistrationRequest, UserRegistrationQuery>();

            // other
            CreateMap<PaginationQueryRequest, PaginationQuery>();
        }
    }
}
