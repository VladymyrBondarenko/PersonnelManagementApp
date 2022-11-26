using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.WebClient.Models
{
    public class DepartmentModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string DepartmentTitle { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string DepartmentDescription { get; set; }

        [Display(Name = "Date From")]
        [Required]
        public DateTime? DateFrom { get; set; } = DateTime.UtcNow;

        public DateTime? DateTo { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
