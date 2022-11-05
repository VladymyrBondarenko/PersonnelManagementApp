using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Routes
{
    public static partial class ApiRoutes
    {
        public static class Orders
        {
            public const string GetAll = $"{BaseUrl}/orders";

            public const string Get = BaseUrl + "/orders/{orderId}";

            public const string Create = $"{BaseUrl}/orders";

            public const string Update = BaseUrl + "/orders/{orderId}";

            public const string Delete = BaseUrl + "/orders/{orderId}";

            public const string AcceptOrder = BaseUrl + "/orders/accept/{orderId}";

            public const string RollbackOrder = BaseUrl + "/orders/rollback/{orderId}";

            public const string AttachFileToOrder = BaseUrl + "/orders/{orderId}/originals/add";
        }
    }
}
