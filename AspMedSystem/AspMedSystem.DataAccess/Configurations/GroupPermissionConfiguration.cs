using AspMedSystem.Domain;
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
            builder.HasOne(groupPermission => groupPermission.Permission).WithMany(permission => permission.GroupPermissions);

            builder.HasOne(groupPermission => groupPermission.Group).WithMany(group => group.GroupPermissions);
        }
    }
}
