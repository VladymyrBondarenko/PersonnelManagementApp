﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Requests.Queries
{
    public class GetAllOrdersQuery
    {
        public string SearchText { get; set; }

        public Guid OrderDescriptionId { get; set; }
    }
}
