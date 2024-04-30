using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakany.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Sakany.Infrastructure.Configurations
{
    internal class TestConfi : IEntityTypeConfiguration<Testentity>
    {
        public void Configure(EntityTypeBuilder<Testentity> builder)
        {
            builder.Property(t => t.Name)
                .HasMaxLength(100);

        }
    }
}
