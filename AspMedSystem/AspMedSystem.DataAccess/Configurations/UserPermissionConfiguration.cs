using AspMedSystem.Domain;
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
            builder.HasOne(userPermission => userPermission.Permission).WithMany(permission => permission.UserPermissions);

            builder.HasOne(UserPermission => UserPermission.User).WithMany(user => user.UserPermissions);
        }
    }
}
