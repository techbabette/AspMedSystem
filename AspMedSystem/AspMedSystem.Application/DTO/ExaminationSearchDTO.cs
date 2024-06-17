using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class ExaminationSearchDTO : PagedSearch
    {
        public int? ExamineeId { get; set; }
        public int? ExaminerId { get; set; }
        public string? ExamineeKeyword { get; set; }
        public string? ExaminerKeyword { get; set; }
        public bool? Performed { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set;}
    }

    public class ExaminationSearchResultDTO
    {
        public int Id { get; set; }
        public int ExamineeId { get; set; }
        public int ExaminerId { get; set; }
        public string ExamineeName { get; set; }
        public string ExamineeEmail { get; set; }
        public string ExaminerName { get; set; }
        public string ExaminerEmail { get; set; }
        public DateTime Date { get; set; }
        public bool Performed { get; set; }
        public int NumberOfReports { get; set; }
    }

    public class ExaminationTermSearchSingleResultDTO : ExaminationSearchResultDTO
    {
        public IEnumerable<int> Reports { get; set; }
    }
}
