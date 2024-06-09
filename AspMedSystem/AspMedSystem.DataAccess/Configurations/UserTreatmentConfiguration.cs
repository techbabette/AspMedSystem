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
    internal class UserTreatmentConfiguration : EntityConfiguration<UserTreatment>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<UserTreatment> builder)
        {
            builder.HasOne(userTreatment => userTreatment.User)
                   .WithMany(user => user.UserTreatments)
                   .HasForeignKey(userTreatment => userTreatment.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(userTreatment => userTreatment.Report)
                   .WithMany(report => report.UserTreatments)
                   .HasForeignKey(userTreatment => userTreatment.ReportId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(userTreatment => userTreatment.Treatment)
                   .WithMany(treatment => treatment.UserTreatments)
                   .HasForeignKey(userTreatment => userTreatment.TreatmentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
