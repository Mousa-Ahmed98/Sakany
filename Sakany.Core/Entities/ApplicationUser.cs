﻿using Microsoft.AspNetCore.Identity;
using Sakany.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? SecondPhoneNumber { get; set; }
        public int? Age { get; set; }
        public Gender? Gender { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public Education? Education { get; set; }
        public string? Employment { get; set; }
        public string? Job { get; set; }
    }
}
