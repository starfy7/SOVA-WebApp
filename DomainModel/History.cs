﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    public class History
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime Access { get; set; }
    }
}
