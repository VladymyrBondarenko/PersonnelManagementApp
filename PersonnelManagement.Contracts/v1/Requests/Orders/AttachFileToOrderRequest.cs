using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Requests.Originals
{
    public class AttachFileToOrderRequest
    {
        public Guid OrderId { get; set; }

        public IFormFile File { get; set; }
    }
}
