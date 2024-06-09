using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Domain
{
    public class Examination : Entity
    {
        public int ExaminationTermId { get; set; }

        public virtual ExaminationTerm ExaminationTerm { get; set; }
        public int ExamineeId { get; set; }
        public virtual User Examinee { get; set; }
        public bool Canceled { get; set; }
        public bool Perfomed { get; set; }
    }
}
