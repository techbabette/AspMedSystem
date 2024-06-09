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
    internal class GroupPermissionConfiguration : EntityConfiguration<GroupPermission>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<GroupPermission> builder)
        {
            builder.Property(groupPermission => groupPermission.Permission)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasOne(groupPermission => groupPermission.Group)
                   .WithMany(group => group.GroupPermissions)
                   .HasForeignKey(groupPermission => groupPermission.GroupId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
