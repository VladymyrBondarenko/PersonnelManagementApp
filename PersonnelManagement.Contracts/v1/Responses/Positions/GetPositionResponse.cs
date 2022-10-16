using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Responses.Positions
{
    public class GetPositionResponse
    {
        public Guid Id { get; set; }

        public string PositionTitle { get; set; }

        public string PositionDescription { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
