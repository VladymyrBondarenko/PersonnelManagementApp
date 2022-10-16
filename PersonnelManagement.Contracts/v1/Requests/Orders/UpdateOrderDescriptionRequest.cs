using PersonnelManagement.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Requests.Orders
{
    public class UpdateOrderDescriptionRequest
    {
        public string OrderDescriptionTitle { get; set; }
    }
}
