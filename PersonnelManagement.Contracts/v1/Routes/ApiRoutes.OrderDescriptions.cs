using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Routes
{
    public static partial class ApiRoutes
    {
        public static class OrderDescriptions
        {
            public const string GetAll = $"{BaseUrl}/orderDescriptions";

            public const string Get = BaseUrl + "/orderDescriptions/{orderDescriptionId}";

            public const string Create = $"{BaseUrl}/orderDescriptions";

            public const string Update = BaseUrl + "/orderDescriptions/{orderDescriptionId}";

            public const string Delete = BaseUrl + "/orderDescriptions/{orderDescriptionId}";
        }
    }
}
