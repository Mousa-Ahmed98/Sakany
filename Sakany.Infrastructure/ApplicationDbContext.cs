using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sakany.Core.Entities;
using System.Reflection;


namespace Sakany.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public  DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public  DbSet<City> Cities { get; set; }
        public  DbSet<Governorate> Governorates { get; set; }
        public  DbSet<Proposal> Proposals { get; set; }

        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
