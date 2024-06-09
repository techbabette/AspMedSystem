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
    internal class UserPermissionConfiguration : EntityConfiguration<UserPermission>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasOne(userPermission => userPermission.Permission)
                   .WithMany(permission => permission.UserPermissions)
                   .HasForeignKey(userPermission => userPermission.PermissionId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(userPermission => userPermission.User)
                   .WithMany(user => user.UserPermissions)
                   .HasForeignKey(userPermission => userPermission.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
