using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.DTOS
{
    public class CityDataDTO
    {
        public int GovernorateID { get; set; }
        public List<string> Cities { get; set; }
    }
}
