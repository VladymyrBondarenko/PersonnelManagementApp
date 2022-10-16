using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Domain.Models.Filters
{
    public class GetAllOrdersFilter
    {
        public string SearchText { get; set; }

        public Guid OrderDescriptionId { get; set; }
    }
}
