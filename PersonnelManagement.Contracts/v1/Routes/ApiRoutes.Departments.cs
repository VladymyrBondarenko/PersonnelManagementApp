using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Routes
{
    public static partial class ApiRoutes
    {
        public static class Departments
        {
            public const string GetAll = $"{BaseUrl}/departments";

            public const string Get = BaseUrl + "/departments/{departmentId}";

            public const string Create = $"{BaseUrl}/departments";

            public const string Update = BaseUrl + "/departments/{departmentId}";

            public const string Delete = BaseUrl + "/departments/{departmentId}";
        }
    }
}
