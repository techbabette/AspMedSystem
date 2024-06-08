using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Domain
{
    public class Permission : NamedEntity
    {
        public virtual ICollection<GroupPermission> GroupPermissions { get; set; } = new HashSet<GroupPermission>();

        public virtual ICollection<UserPermission> UserPermissions { get; set; } = new HashSet<UserPermission>();
    }
}
