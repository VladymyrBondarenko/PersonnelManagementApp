using AutoMapper;
using PersonnelManagement.Contracts.v1.Requests.Queries;
using PersonnelManagement.Contracts.v1.Responses.Departments;
using PersonnelManagement.Contracts.v1.Responses.Orders;
using PersonnelManagement.Contracts.v1.Responses.OrdersDescription;
using PersonnelManagement.Contracts.v1.Responses.Positions;
using PersonnelManagement.Domain.Orders;
using static PersonnelManagement.WebClient.Pages.Orders.Orders;
using static PersonnelManagement.WebClient.Pages.Orders.OrdersDescription;
using static PersonnelManagement.WebClient.Pages.OrgStruct.Departments;
using static PersonnelManagement.WebClient.Pages.OrgStruct.Positions;

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
                        OrderDescriptionId = x.OrderDescriptionId,
                        //OrderDescription = new OrderDescriptionModel
                        //{
                        //    Id = x.OrderDescriptionId,
                        //    OrderDescriptionTitle = x.OrderDescription.OrderDescriptionTitle,
                        //    OrderType = x.OrderDescription.OrderType
                        //}
                    }));
                });

            CreateMap<GetOrderResponse, OrderModel>()
                .ForMember(x => x.Position, opt =>
                {
                    opt.MapFrom(src => new GetPositionResponse { Id = src.Id, PositionTitle = src.Position.PositionTitle });
                })
                .ForMember(x => x.Department, opt =>
                {
                    opt.MapFrom(src => new GetDepartmentResponse { Id = src.Department.Id, DepartmentTitle = src.Department.DepartmentTitle });
                });
                //.ForMember(x => x.OrderDescription, opt =>
                //{
                //    opt.MapFrom(src => new OrderDescriptionModel
                //    {
                //        Id = src.OrderDescriptionId,
                //        OrderDescriptionTitle = src.OrderDescription.OrderDescriptionTitle,
                //        OrderType = src.OrderDescription.OrderType
                //    });
                //});

            CreateMap<GetDepartmentResponse, DepartmentModel>();

            CreateMap<GetPositionResponse, PositionModel>();
        }
    }
}
