using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.DTOS
{
    public class CustomResponseDTO
    {      
        public bool Success { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
