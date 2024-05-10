using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.DTOS
{
    public class PropertyDTO
    {
      public  List<string> featsures {  get; set; }= new List<string>();
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int RoomsNumber { get; set; }
        public decimal Price { get; set; }
        public decimal Area { get; set; }
        public int Governorate { get; set; }
        public string City { get; set; }
        public string Age { get; set; }
        public int BathroomNumber { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
