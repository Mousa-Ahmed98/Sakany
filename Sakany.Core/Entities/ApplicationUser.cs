using Microsoft.AspNetCore.Identity;
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

        public string Name { get; set; }

        public string? SecondPhoneNumber { get; set; }
        public int? Age { get; set; }
        public Gender? Gender { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public Education? Education { get; set; }
        public string? Employment { get; set; }
        public string? Job { get; set; }

        public ApplicationUser ExteractInfo(ApplicationUser appUser)
        {
            this.SecondPhoneNumber = appUser.SecondPhoneNumber;
            this.Age = appUser.Age;
            this.Gender = appUser.Gender;
            this.MaritalStatus = appUser.MaritalStatus;
            this.Education = appUser.Education;
            this.Employment = appUser.Employment;
            this.Job = appUser.Job;

            return this;
        }
    }
}
