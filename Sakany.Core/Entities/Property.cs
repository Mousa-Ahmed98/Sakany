using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Core.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string status { get; set; }
        public string Type { get; set; }
            
        public int RoomsNumber { get; set; }
        public float Price { get; set; }
        public float Area { get; set;}
        //images in  new Table


        

    }
}
