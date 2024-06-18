using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class UserTreatmentCreateDTO
    {
        public int ReportId { get; set; }
        public int TreatmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Note { get; set; }
    }

    public class UserTreatmentUpdateDTO : UserTreatmentCreateDTO
    {
        public int Id { get; set; }
    }
}
