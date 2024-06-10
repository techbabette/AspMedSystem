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
    internal class UseCaseLogConfiguration : EntityConfiguration<UseCaseLog>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<UseCaseLog> builder)
        {
            builder.Property(useCaseLog => useCaseLog.UseCaseName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(useCaseLog => useCaseLog.ActorEmail)
                   .IsRequired()
                   .HasMaxLength(254);

            builder.HasIndex(useCaseLog => new { useCaseLog.ActorEmail, useCaseLog.UseCaseName, useCaseLog.CreatedAt });
        }
    }
}
