using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Routes
{
    public static partial class ApiRoutes
    {
        public static class Identity
        {
            public const string Register = $"{BaseUrl}/identity/register";

            public const string Login = $"{BaseUrl}/identity/login";

            public const string Refresh = $"{BaseUrl}/identity/refresh";
        }
    }
}
