using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Domain
{
    public class Treatment : NamedEntity
    {
        public virtual ICollection<TreatmentCounterindication> CounterIndicatedBy { get; set; } = new HashSet<TreatmentCounterindication>();
        public virtual ICollection<TreatmentCounterindication> CounterIndicates { get; set; } = new HashSet<TreatmentCounterindication>();
    }
}
