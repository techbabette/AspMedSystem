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
    internal class ExaminationConfiguration : EntityConfiguration<Examination>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Examination> builder)
        {
            builder.HasOne(examination => examination.Examinee)
                   .WithMany(examinee => examinee.Examinations)
                   .HasForeignKey(examination => examination.ExamineeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(examination => examination.ExaminationTerm)
                   .WithMany(examinationTerm => examinationTerm.Examinations)
                   .HasForeignKey(examination => examination.ExaminationTermId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
