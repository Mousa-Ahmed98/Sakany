using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.DTOS
{
    public class PropertiesDetilesDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int RoomsNumber { get; set; }
        public decimal Price { get; set; }
        public decimal Area { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public int BathroomNumber { get; set; }
        public List<string> Images { get; set; }

    }
}
