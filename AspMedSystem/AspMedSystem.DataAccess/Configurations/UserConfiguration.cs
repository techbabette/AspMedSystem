using AspMedSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.DataAccess.Configurations
{
    internal class UserConfiguration : EntityConfiguration<User>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Email)
                   .HasMaxLength(254)
                   .IsRequired();
            builder.HasIndex(user => user.Email)
                   .IsUnique();

            builder.Property(user => user.FirstName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(user => user.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(user => user.Password)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.HasIndex(user => new { user.FirstName, user.LastName, user.Email, })
                   .IncludeProperties(user => new { user.BirthDate });
        }
    }
}
