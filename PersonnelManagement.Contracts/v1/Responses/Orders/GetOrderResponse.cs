using PersonnelManagement.Contracts.v1.Responses.Departments;
using PersonnelManagement.Contracts.v1.Responses.Employees;
using PersonnelManagement.Contracts.v1.Responses.OrdersDescription;
using PersonnelManagement.Contracts.v1.Responses.Originals;
using PersonnelManagement.Contracts.v1.Responses.Positions;
using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Orders;
using PersonnelManagement.Domain.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Responses.Orders
{
    public class GetOrderResponse
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid? EmployeeId { get; set; }

        public GetEmployeeResponse Employee { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public Guid? DepartmentId { get; set; }

        public GetDepartmentResponse Department { get; set; }

        public Guid? PositionId { get; set; }

        public GetPositionResponse Position { get; set; }

        public Guid OrderDescriptionId { get; set; }

        //public GetOrderDescriptionResponse OrderDescription { get; set; }

        public OrderState OrderState { get; set; }

        public List<GetOriginalResponse> Originals { get; set; }
    }
}
