using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.Interfaces
{
    public interface ICityRepository
    {
        public List<City> GetAll(int governorateI);
        public City GetById(int Id);
       
    }
}
