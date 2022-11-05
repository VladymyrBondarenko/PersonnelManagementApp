using AutoMapper;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses.Departments;
using PersonnelManagement.Contracts.v1.Responses.Employees;
using PersonnelManagement.Contracts.v1.Responses.Orders;
using PersonnelManagement.Contracts.v1.Responses.OrdersDescription;
using PersonnelManagement.Contracts.v1.Responses.Originals;
using PersonnelManagement.Contracts.v1.Responses.Positions;
using PersonnelManagement.Domain.Orders;
using static PersonnelManagement.WebClient.Pages.Employees.Employees;
using static PersonnelManagement.WebClient.Pages.Orders.Orders;
using static PersonnelManagement.WebClient.Pages.Orders.OrdersDescription;
using static PersonnelManagement.WebClient.Pages.OrgStruct.Departments;
using static PersonnelManagement.WebClient.Pages.OrgStruct.Positions;
using static PersonnelManagement.WebClient.Pages.Originals.Originals;

namespace PersonnelManagement.WebClient.MappingProfiles
{
    public class ResponseToModelProfile : Profile
    {
        public ResponseToModelProfile()
        {
            CreateMap<GetOrderDescriptionResponse, OrderDescriptionModel>()
                .ForMember(x => x.Orders, opt =>
                {
                    opt.MapFrom(src => src.Orders.Select(x => new OrderModel
                    {
                        Id = x.Id,
                        DateFrom = x.DateFrom,
                        DateTo = x.DateTo,
                        DepartmentId = x.DepartmentId,
                        Department = x.Department != null ? new DepartmentModel { Id = x.Department.Id, DepartmentTitle = x.Department.DepartmentTitle } : null,
                        PositionId = x.PositionId,
                        Position = x.Position != null ? new PositionModel { Id = x.Position.Id, PositionTitle = x.Position.PositionTitle } : null,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        OrderState = x.OrderState,
                        OrderDescriptionId = x.OrderDescriptionId
                    }));
                });

            CreateMap<GetOrderResponse, OrderModel>()
                .ForMember(x => x.Position, opt =>
                {
                    opt.MapFrom(src => new PositionModel { Id = src.Id, PositionTitle = src.Position.PositionTitle });
                })
                .ForMember(x => x.Department, opt =>
                {
                    opt.MapFrom(src => new DepartmentModel { Id = src.Department.Id, DepartmentTitle = src.Department.DepartmentTitle });
                })
                .ForMember(x => x.Employee, opt =>
                {
                    opt.MapFrom(src => new EmployeeModel 
                    { 
                        Id = src.Employee != null ? src.Employee.Id : default,
                        DepartmentId = src.DepartmentId,
                        Department = src.Department != null ? new DepartmentModel { Id = src.Department.Id, DepartmentTitle = src.Department.DepartmentTitle } : default,
                        PositionId = src.PositionId,
                        Position = src.Position != null ? new PositionModel { Id = src.Position.Id, PositionTitle = src.Position.PositionTitle } : default,
                        EmployeeState = src.Employee != null ? src.Employee.EmployeeState : default,
                        FirstName = src.Employee != null ? src.Employee.FirstName : default,
                        LastName = src.Employee != null ? src.Employee.LastName : default,
                        FireDate = src.Employee != null ? src.Employee.FireDate : default,
                        HireDate = src.Employee != null ? src.Employee.HireDate : default
                    });
                })
                .ForMember(x => x.Originals, opt =>
                {
                    opt.MapFrom(src => src.Originals.Select(x => new OriginalModel 
                    { 
                        Id = x.Id,
                        OriginalFileExtension = x.OriginalFileExtension,
                        OriginalTitle = x.OriginalTitle,
                        OriginalPath = x.OriginalPath
                    }));
                });

            CreateMap<GetDepartmentResponse, DepartmentModel>();

            CreateMap<GetPositionResponse, PositionModel>();

            CreateMap<GetEmployeeResponse, EmployeeModel>();

            CreateMap<GetOriginalResponse, OriginalModel>();
        }
    }
}
