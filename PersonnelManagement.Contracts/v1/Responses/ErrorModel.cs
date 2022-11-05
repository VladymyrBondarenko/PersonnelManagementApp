using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Responses
{
    public class ErrorModel
    {
        public string FieldName { get; set; }

        public string Message { get; set; }
    }
}
