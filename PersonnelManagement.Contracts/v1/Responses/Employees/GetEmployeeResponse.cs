using PersonnelManagement.Contracts.v1.Responses.Departments;
using PersonnelManagement.Contracts.v1.Responses.Orders;
using PersonnelManagement.Contracts.v1.Responses.Positions;
using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Models.Originals;
using PersonnelManagement.Domain.Orders;
using PersonnelManagement.Domain.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Responses.Employees
{
    public class GetEmployeeResponse
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime FireDate { get; set; }

        public List<GetOrderResponse> Orders { get; set; }

        public Guid? DepartmentId { get; set; }

        public GetDepartmentResponse Department { get; set; }

        public Guid? PositionId { get; set; }

        public GetPositionResponse Position { get; set; }

        public EmployeeState EmployeeState { get; set; }
    }
}
