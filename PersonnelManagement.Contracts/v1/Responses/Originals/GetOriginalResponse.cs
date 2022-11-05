using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Responses.Originals
{
    public class GetOriginalResponse
    {
        public Guid Id { get; set; }

        public string OriginalTitle { get; set; }

        public string OriginalPath { get; set; }

        public string OriginalFileExtension { get; set; }
    }
}
