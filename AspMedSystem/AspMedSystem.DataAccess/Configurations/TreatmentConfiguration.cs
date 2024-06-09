using AspMedSystem.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.DataAccess.Configurations
{
    internal class TreatmentConfiguration : NamedEntityConfiguration<Treatment>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Treatment> builder)
        {
            base.ConfigureEntity(builder);
        }
    }
}
