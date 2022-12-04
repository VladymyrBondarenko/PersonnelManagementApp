using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Routes
{
    public static partial class ApiRoutes
    {
        public static class Originals
        {
            public const string GetAll = $"{BaseUrl}/originals";

            public const string Get = BaseUrl + "/originals/{originalId}";

            public const string Update = BaseUrl + "/originals/{originalId}";

            public const string Create = BaseUrl + "/originals/{originalEntity}/{originalType}/{entityId}";

            public const string DownloadFile = BaseUrl + "/originals/download/{originalId}";

            public const string Delete = BaseUrl + "/originals/{originalId}";
        }
    }
}
