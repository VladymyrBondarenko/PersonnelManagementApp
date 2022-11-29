using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Domain.Orders
{
    public class OrderDescription
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string OrderDescriptionTitle { get; set; }

        public OrderType OrderType { get; set; }

        public ICollection<Order> Orders { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
