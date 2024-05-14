﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public  DbSet<Message> Messages { get; set; }
        public  DbSet<Properties> Properties { get; set; }
        public  DbSet<PropertyFeatures> PropertyFeatures { get; set; }
        public  DbSet<PropertyImage> PropertyImages { get; set; }


        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
