using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class ExaminationTermSearchDTO : PagedSearch
    {
        public int? ExaminerId { get; set; }
        public string? Keyword { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public bool? Available { get; set; }
    }

    public class ExaminationTermSearchResultDTO
    {
        public int Id { get; set; }
        public int ExaminerId { get; set; }
        public string ExaminerName { get; set; }
        public string ExaminerEmail { get; set; }
        public DateTime TermDate { get; set; }
        public bool Available { get; set; }
    }
}
