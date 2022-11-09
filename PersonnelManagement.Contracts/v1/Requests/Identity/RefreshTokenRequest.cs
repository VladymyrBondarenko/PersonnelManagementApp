using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Requests.Identity
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; }

        public Guid RefreshToken { get; set; }
    }
}
