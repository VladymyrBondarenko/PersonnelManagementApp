using PersonnelManagement.Domain.Orders;
using System.ComponentModel.DataAnnotations;
using static PersonnelManagement.WebClient.Pages.Employees.Employees;

namespace PersonnelManagement.WebClient.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Date From")]
        [Required]
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        [Display(Name = "Department")]
        [Required]
        public DepartmentModel Department { get; set; }

        [Required]
        public Guid? DepartmentId { get; set; }

        [Display(Name = "Position")]
        [Required]
        public PositionModel Position { get; set; }

        [Required]
        public Guid? PositionId { get; set; }

        [Required]
        public OrderDescriptionModel OrderDescription { get; set; }

        [Required]
        public Guid? OrderDescriptionId { get; set; }

        public Guid? EmployeeId { get; set; }

        [Display(Name = "Emplpoyee")]
        [Required]
        public EmployeeModel Employee { get; set; }

        public OrderState OrderState { get; set; }

        public List<OriginalModel> Originals { get; set; }
    }
}
