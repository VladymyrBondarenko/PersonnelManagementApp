using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Requests.Identity
{
    public class UserRegistrationRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
