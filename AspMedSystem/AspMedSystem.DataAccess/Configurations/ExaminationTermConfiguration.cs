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
    internal class ExaminationTermConfiguration : EntityConfiguration<ExaminationTerm>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<ExaminationTerm> builder)
        {
            builder.HasOne(examinationTerm => examinationTerm.Examiner)
                   .WithMany(examiner => examiner.ExaminationTerms)
                   .HasForeignKey(examinationTerm => examinationTerm.ExaminerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
