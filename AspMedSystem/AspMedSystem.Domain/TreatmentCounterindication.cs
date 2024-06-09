using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Domain
{
    public class TreatmentCounterindication : Entity
    {
        public int TreatmentId { get; set; }

        public int CounterIndicatedTreatmentId { get; set; }

        public virtual Treatment Treatment { get; set; }

        public virtual Treatment CounterIndicatedTreatment { get; set; }
    }
}
