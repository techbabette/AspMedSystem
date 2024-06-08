using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Domain
{
    public class GroupPermission : Entity
    {
        public int GroupId { get; set; }

        public int PermissionId { get; set;}

        public virtual Group Group { get; set; }

        public virtual Permission Permission { get; set; }
        public bool Effect { get; set; }

    }
}
