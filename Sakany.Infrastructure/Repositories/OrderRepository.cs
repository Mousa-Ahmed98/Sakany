using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sakany.Application.DTOS;
using Sakany.Application.Interfaces;
using Sakany.Core.Entities;
namespace Sakany.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private ApplicationDbContext context;

        public OrderRepository(ApplicationDbContext Context)
        {
            context = Context;
        }

        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            return await context.Orders.ToListAsync();
        }
    }
}
