﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Domain.Positions
{
    public class Position
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string PositionTitle { get; set; }
    }
}
