using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.WebClient.Models
{
    public class PositionModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string PositionTitle { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string PositionDescription { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
