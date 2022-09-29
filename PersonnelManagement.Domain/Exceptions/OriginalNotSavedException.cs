using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Domain.Exceptions
{
    public class OriginalNotSavedException : Exception
    {
        public OriginalNotSavedException(string message) : base(message)
        {

        }
    }
}
