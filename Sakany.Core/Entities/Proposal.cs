using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Core.Entities
{
    public class Proposal
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public int PropertiesId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public Properties Property { get; set; }
    }
}
