using AutoMapper;
using PersonnelManagement.Contracts.v1.Responses.Departments;
using PersonnelManagement.Contracts.v1.Responses.Employees;
using PersonnelManagement.Contracts.v1.Responses.Orders;
using PersonnelManagement.Contracts.v1.Responses.OrdersDescription;
using PersonnelManagement.Contracts.v1.Responses.Originals;
using PersonnelManagement.Contracts.v1.Responses.Positions;
using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Models.Originals;
using PersonnelManagement.Domain.Orders;
using PersonnelManagement.Domain.Positions;

namespace PersonnelManagement.Api.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Department, GetDepartmentResponse>();

            CreateMap<Position, GetPositionResponse>();

            CreateMap<Original, GetOriginalResponse>();

            CreateMap<OrderDescription, GetOrderDescriptionResponse>();

            CreateMap<Employee, GetEmployeeResponse>()
                .ForMember(x => x.Position, opt =>
                {
                    opt.MapFrom(src => new GetPositionResponse { Id = src.Position.Id, PositionTitle = src.Position.PositionTitle });
                })
                .ForMember(x => x.Department, opt =>
                {
                    opt.MapFrom(src => new GetDepartmentResponse { Id = src.Department.Id, DepartmentTitle = src.Department.DepartmentTitle });
                })
                .ForMember(x => x.Orders, opt =>
                {
                    opt.MapFrom(src => src.Orders.Select(x => new GetOrderResponse
                    {
                        Id = x.Id,
                        DateFrom = x.DateFrom,
                        DateTo = x.DateTo,
                        Department = new GetDepartmentResponse { Id = x.Department.Id, DepartmentTitle = x.Department.DepartmentTitle },
                        Position = new GetPositionResponse { Id = src.Position.Id, PositionTitle = src.Position.PositionTitle },
                        DepartmentId = x.DepartmentId,
                        PositionId = x.PositionId,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        EmployeeId = x.EmployeeId,
                        OrderState = x.OrderState,
                        OrderDescriptionId = x.OrderDescriptionId,
                        Employee = new GetEmployeeResponse
                        {
                            Id = src.Id,
                            HireDate = src.HireDate,
                            FireDate = src.FireDate,
                            Department = new GetDepartmentResponse { Id = src.Department.Id, DepartmentTitle = src.Department.DepartmentTitle },
                            Position = new GetPositionResponse { Id = src.Position.Id, PositionTitle = src.Position.PositionTitle },
                            DepartmentId = src.DepartmentId,
                            PositionId = src.PositionId,
                            EmployeeState = src.EmployeeState,
                            FirstName = src.FirstName,
                            LastName = src.LastName
                        }
                    }));
                })
                .ForMember(x => x.Originals, opt =>
                {
                    opt.MapFrom(src => src.Originals.Select(x => new GetOriginalResponse
                    {
                        Id = x.Id,
                        OriginalTitle = x.OriginalTitle,
                        OriginalPath = x.OriginalPath,
                        OriginalFileExtension = x.OriginalFileExtension
                    }));
                });

            CreateMap<Order, GetOrderResponse>()
                .ForMember(x => x.Position, opt =>
                {
                    opt.MapFrom(src => new GetPositionResponse { Id = src.Position.Id, PositionTitle = src.Position.PositionTitle });
                })
                .ForMember(x => x.Department, opt =>
                {
                    opt.MapFrom(src => new GetDepartmentResponse { Id = src.Department.Id, DepartmentTitle = src.Department.DepartmentTitle });
                })
                .ForMember(x => x.Employee, opt => 
                {
                    opt.MapFrom(src => new GetEmployeeResponse
                    {
                        Id = src.Employee.Id,
                        HireDate = src.Employee.HireDate,
                        FireDate = src.Employee.FireDate,
                        Department = new GetDepartmentResponse { Id = src.Department.Id, DepartmentTitle = src.Department.DepartmentTitle },
                        Position = new GetPositionResponse { Id = src.Position.Id, PositionTitle = src.Position.PositionTitle },
                        DepartmentId = src.Employee.DepartmentId,
                        PositionId = src.Employee.PositionId,
                        EmployeeState = src.Employee.EmployeeState,
                        FirstName = src.Employee.FirstName,
                        LastName = src.Employee.LastName,
                        Orders = src.Employee.Orders.Select(x => new GetOrderResponse
                        {
                            Id = x.Id,
                            DateFrom = x.DateFrom,
                            DateTo = x.DateTo,
                            Department = new GetDepartmentResponse { Id = x.Department.Id, DepartmentTitle = x.Department.DepartmentTitle },
                            Position = new GetPositionResponse { Id = src.Position.Id, PositionTitle = src.Position.PositionTitle },
                            DepartmentId = x.DepartmentId,
                            PositionId = x.PositionId,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            OrderState = x.OrderState,
                            OrderDescriptionId = x.OrderDescriptionId
                        }).ToList()
                    });
                })
                .ForMember(x => x.Originals, opt =>
                {
                    opt.MapFrom(src => src.Originals.Select(x => new GetOriginalResponse 
                    {
                        Id = x.Id,
                        OriginalTitle = x.OriginalTitle,
                        OriginalPath = x.OriginalPath,
                        OriginalFileExtension = x.OriginalFileExtension
                    }));
                });
        }
    }
}
