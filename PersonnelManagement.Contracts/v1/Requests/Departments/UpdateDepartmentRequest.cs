namespace PersonnelManagement.Contracts.v1.Responses.Departments
{
    public class UpdateDepartmentRequest
    {
        public string DepartmentTitle { get; set; }

        public string DepartmentDescription { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
