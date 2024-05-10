using Microsoft.EntityFrameworkCore;
using Sakany.Application.Interfaces;
using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Infrastructure.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly ApplicationDbContext dbContext;

        public PropertyRepository(ApplicationDbContext dbContext )
        {
            this.dbContext = dbContext;
        }
        public async Task<Properties> AddAsync(Properties property)
        {
           await dbContext.Set<Properties>().AddAsync(property);
            dbContext.SaveChanges();
            return property;
        }

        public bool Delete(Properties property)
        {
            try
            {
                dbContext.Set<Properties>().Remove(property);
                dbContext.SaveChanges();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public async Task<List<Properties>> GetAllAsync()
        {
            return  await dbContext.Set<Properties>()
                .ToListAsync();
        }

        public async Task<Properties> GetByIdAsync(int propertyID)
        {
            Properties? property= await dbContext.Set<Properties>()
                .FirstOrDefaultAsync(p=>p.Id==propertyID);

            return property;
        }

        public  void Update(Properties property)
        { 
            dbContext.Set<Properties>().Update(property);
            dbContext.SaveChanges();
            
        }
    }
}
