﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Core.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int UserId { get; set; }  
    }
}
