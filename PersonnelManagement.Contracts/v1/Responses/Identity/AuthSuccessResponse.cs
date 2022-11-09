using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Responses.Identity
{
    public class AuthSuccessResponse
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
