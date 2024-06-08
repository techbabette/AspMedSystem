using AspMedSystem.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.DataAccess.Configurations
{
    internal abstract class NamedEntityConfiguration<T> : EntityConfiguration<T> where T : NamedEntity
    {
        protected override void ConfigureEntity(EntityTypeBuilder<T> builder)
        {
            builder.Property(namedEntity => namedEntity.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(namedEntity => namedEntity.Name);
        }
    }
}
