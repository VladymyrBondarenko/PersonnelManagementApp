using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Requests.Positions
{
    public class CreatePositionRequest
    {
        public string PositionTitle { get; set; }

        public string PositionDescription { get; set; }
    }
}
