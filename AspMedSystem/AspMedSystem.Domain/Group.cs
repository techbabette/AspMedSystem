using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Domain
{
    public class Group : NamedEntity
    {
        public virtual ICollection<GroupPermission> GroupPermissions { get; set; } = new HashSet<GroupPermission>();
    }
}
