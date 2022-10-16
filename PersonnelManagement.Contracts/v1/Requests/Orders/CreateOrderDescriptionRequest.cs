using PersonnelManagement.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Requests.Orders
{
    public class CreateOrderDescriptionRequest
    {
        public string OrderDescriptionTitle { get; set; }

        public int OrderType { get; set; }
    }
}
