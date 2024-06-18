using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Domain
{
    public class UserTreatment : Entity
    {
        public int UserId { get; set; }

        public int ReportId { get; set; }

        public int TreatmentId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        
        public string? Note { get; set; }

        public virtual User User { get; set; }

        public virtual Report Report { get; set; }

        public virtual Treatment Treatment { get; set; }
    }
}
