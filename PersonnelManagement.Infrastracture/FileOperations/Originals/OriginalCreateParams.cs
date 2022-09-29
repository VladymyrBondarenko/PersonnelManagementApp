using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Infrastracture.FileOperations.Originals
{
    public class OriginalCreateParams
    {
        public string SourceFilePath { get; set; }

        public Guid OrderId { get; set; }

        public string EmployeeId { get; set; }
    }
}
