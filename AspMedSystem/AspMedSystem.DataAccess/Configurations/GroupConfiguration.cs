using AspMedSystem.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.DataAccess.Configurations
{
    internal class GroupConfiguration : EntityConfiguration<Group>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Group> builder)
        {
            builder.Property(group => group.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(group => group.Name);
        }
    }
}
