using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Requests.Originals
{
    public class DeleteAttachmentRequest
    {
        public Guid OriginalId { get; set; }
    }
}
