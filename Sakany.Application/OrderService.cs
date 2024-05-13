using Microsoft.AspNetCore.Mvc;
using Sakany.Application.Interfaces;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application
{
    public class OrderService : IOrderService
    {
        private  IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            return _orderRepository.GetAll();
        }
    }
}
