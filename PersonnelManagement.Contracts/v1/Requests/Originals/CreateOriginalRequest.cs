using Microsoft.AspNetCore.Http;
using PersonnelManagement.Domain.Models.Originals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Requests.Originals
{
    public class CreateOriginalRequest
    {
        public int OriginalEntity { get; set; }

        public int OriginalEntityId { get; set; }

        public Guid EntityId { get; set; }

        public IFormFile File { get; set; }
    }
}
