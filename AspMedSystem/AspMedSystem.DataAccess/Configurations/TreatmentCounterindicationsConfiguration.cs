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
    internal class TreatmentCounterindicationsConfiguration : EntityConfiguration<TreatmentCounterindication>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<TreatmentCounterindication> builder)
        {
            builder.HasOne(treatmentCounterindication => treatmentCounterindication.Treatment)
                   .WithMany(treatment => treatment.CounterIndicatedBy)
                   .HasForeignKey(treatmentCounterIndication => treatmentCounterIndication.TreatmentId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(treatmentCounterindication => treatmentCounterindication.CounterIndicatedTreatment)
                   .WithMany(counterIndicativeTreatment => counterIndicativeTreatment.CounterIndicates)
                   .HasForeignKey(treatmentCounterindication => treatmentCounterindication.CounterIndicatedTreatmentId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
