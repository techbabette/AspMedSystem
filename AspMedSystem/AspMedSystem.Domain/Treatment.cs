using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Domain
{
    public class Treatment : NamedEntity
    {
        public virtual ICollection<UserTreatment> UserTreatments { get; set; } = new HashSet<UserTreatment>();
        public bool Prescribable { get; set; }
    }
}
