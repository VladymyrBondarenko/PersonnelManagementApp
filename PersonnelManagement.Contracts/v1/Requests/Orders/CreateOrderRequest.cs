namespace PersonnelManagement.Contracts.v1.Requests.Orders
{
    public class CreateOrderRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public Guid? DepartmentId { get; set; }

        public Guid? PositionId { get; set; }

        public Guid? EmployeeId { get; set; }

        public Guid OrderDescriptionId { get; set; }
    }
}
