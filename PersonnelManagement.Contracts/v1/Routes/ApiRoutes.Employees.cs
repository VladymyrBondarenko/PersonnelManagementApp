using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Routes
{
    public static partial class ApiRoutes
    {
        public static class Employees
        {
            public const string GetAll = $"{BaseUrl}/employees";

            public const string Get = BaseUrl + "/employees/{employeeId}";

            public const string Create = $"{BaseUrl}/employees";

            public const string Update = BaseUrl + "/employees/{employeeId}";

            public const string Delete = BaseUrl + "/employees/{employeeId}";
        }
    }
}
