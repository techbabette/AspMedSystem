using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Domain
{
    public abstract class PermissionEffect : Entity
    {
        public int PermissionId { get; set; }

        public virtual Permission Permission { get; set; }
        public bool Effect { get; set; }
    }
}
