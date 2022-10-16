using PersonnelManagement.Contracts.v1.Responses.Orders;
using PersonnelManagement.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Responses.OrdersDescription
{
    public class GetOrderDescriptionResponse
    {
        public Guid Id { get; set; }

        public List<GetOrderResponse> Orders { get; set; }

        public string OrderDescriptionTitle { get; set; }

        public OrderType OrderType { get; set; }
    }
}
