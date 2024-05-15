using Microsoft.AspNetCore.Mvc;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.Interfaces
{
    public interface IOrderRepository
    {
        //get all orders
        public Task<ActionResult<IEnumerable<Order>>> GetAll();
    }
}
