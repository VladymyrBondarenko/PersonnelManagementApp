using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.WebClient.Models
{
    public class OriginalModel
    {
        public Guid? Id { get; set; }

        [Display(Name = "Title")]
        public string OriginalTitle { get; set; }

        public string OriginalPath { get; set; }

        [Display(Name = "Ext")]
        public string OriginalFileExtension { get; set; }

        public int OriginalType { get; set; }
    }
}
