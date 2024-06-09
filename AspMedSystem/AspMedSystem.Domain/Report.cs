using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Domain
{
    public class Report : Entity
    {
        public int ExaminationId { get; set; }

        [Column(TypeName="text")]
        public string Text { get; set; }

        public virtual Examination Examination { get; set; }

        public virtual ICollection<UserTreatment> UserTreatments { get; set; } = new HashSet<UserTreatment>();
    }
}
