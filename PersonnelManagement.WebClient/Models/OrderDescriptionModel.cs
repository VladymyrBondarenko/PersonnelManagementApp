using PersonnelManagement.Domain.Orders;
using System.ComponentModel.DataAnnotations;

namespace PersonnelManagement.WebClient.Models
{
    public class OrderDescriptionModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Type")]
        [Required]
        public OrderType OrderType { get; set; }

        [Display(Name = "Title")]
        [Required]
        public string OrderDescriptionTitle { get; set; }

        public List<OrderModel> Orders { get; set; }
    }
}
