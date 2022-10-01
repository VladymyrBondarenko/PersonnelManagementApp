using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Application.FileOperations.Originals
{
    public class OriginalCreateParams
    {
        public string SourceFilePath { get; set; }

        public byte[] Bytes { get; set; }

        public string FileName { get; set; }

        public Guid OrderId { get; set; }

        public Guid EmployeeId { get; set; }
    }
}
