using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Domain.Models.Filters
{
    public class GetAllOriginalsFilter
    {
        public Guid EntityKey { get; set; }

        public string SearchText { get; set; }
    }
}
