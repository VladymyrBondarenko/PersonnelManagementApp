using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Requests.Queries
{
    public class GetAllOriginalsQuery
    {
        public Guid EntityKey { get; set; }

        public string SearchText { get; set; }
    }
}
