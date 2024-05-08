using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sakany.Application.DTOS;

namespace Sakany.Infrastructure.Configurations
{
    //internal class RegisterConfi : IEntityTypeConfiguration<RegisterUserDTO>
    //{
    //    public void Configure(EntityTypeBuilder<RegisterUserDTO> builder)
    //    {
            //builder.HasNoKey();
            //builder.Property(p => p.UserName).IsRequired().HasAnnotation("Description","This field is requierd please enter user name");
            //builder.Property(p => p.Password)
            //     .IsRequired()
            //     .HasAnnotation("Description", "This field is required. Please enter password")
            //     .HasAnnotation("Description", "password must be less than 12 digits");

            //builder.Property(p => p.Email)
            //    .IsRequired()
            //    .HasAnnotation("Description", "This field is requierd please enter your email"); 

            //builder.Property(p => p.PhoneNumber).IsRequired().HasAnnotation("Description", "This field is requierd please enter user name");
                

    //    }
    //}
}
