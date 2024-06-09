using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Domain
{
    public class ExaminationTerm : Entity
    {
        public int ExaminerId { get; set; }

        public virtual User Examiner { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<Examination> Examinations { get; set; } = new HashSet<Examination>();
    }
}
