using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int OwnerID { get; set; }
        public int AsetID { get; set; }
        public int Proposal { get; set; }
        public DateTime Date { get; set; }
        public bool IsApproved { get; set; }

    }
}
