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
    internal class ReportConfiguration : EntityConfiguration<Report>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Report> builder)
        {
            builder.HasOne(report => report.Examination)
                   .WithMany(examination => examination.Reports)
                   .HasForeignKey(report => report.ExaminationId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
