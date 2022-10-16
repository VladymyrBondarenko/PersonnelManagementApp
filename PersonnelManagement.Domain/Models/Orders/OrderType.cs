using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Domain.Orders
{
    public enum OrderType
    {
        [Display(Name = "Hire Order")]
        HireOrder,

        [Display(Name = "Fire Order")]
        FireOrder
    }
}
