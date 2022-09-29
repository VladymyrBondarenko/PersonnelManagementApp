using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Application.FileOperations.Originals
{
    public class OriginalGetParams
    {
        public Guid OrderId { get; set; }

        public Guid EmployeeId { get; set; }
    }
}
