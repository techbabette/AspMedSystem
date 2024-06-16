using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class ReportSearchDTO : PagedSearch
    {
        public int? ExaminerId { get; set; }
        public int? ExamineeId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
