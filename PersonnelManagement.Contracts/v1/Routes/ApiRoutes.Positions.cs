using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Routes
{
    public static partial class ApiRoutes
    {
        public static class Positions
        {
            public const string GetAll = $"{BaseUrl}/positions";

            public const string Get = BaseUrl + "/positions/{positionId}";

            public const string Create = $"{BaseUrl}/positions";

            public const string Update = BaseUrl + "/positions/{positionId}";

            public const string Delete = BaseUrl + "/positions/{positionId}";
        }
    }
}
