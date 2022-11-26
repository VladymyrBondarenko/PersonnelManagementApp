using PersonnelManagement.Domain.Employees;
using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.WebClient.Models
{
    public class EmployeeModel
    {
        public Guid Id { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Hire Date")]
        [Required]
        public DateTime? HireDate { get; set; }

        [Display(Name = "Fire Date")]
        public DateTime? FireDate { get; set; }

        [Display(Name = "Department")]
        [Required]
        public DepartmentModel Department { get; set; }

        public Guid? DepartmentId { get; set; }

        [Display(Name = "Position")]
        [Required]
        public PositionModel Position { get; set; }

        public Guid? PositionId { get; set; }

        public EmployeeState EmployeeState { get; set; }

        public List<OriginalModel> Originals { get; set; }
    }
}
