using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Core.Entities
{
    public class Properties
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int RoomsNumber { get; set; }
        public int BathroomNumber { get; set; }
        public float Age { get; set; }
        public float Price { get; set; }
        public float Area { get; set; }
        public int GovernorateID { get; set; }
        public int City { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }


        public List<Proposal> Proposals { get; set; }

        public DateTime Date { get; set; }
        

    }

}
