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
        public int? ExaminationId { get; set; }
        public string? ExamineeKeyword { get; set; }
        public string? ExaminerKeyword { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    public class ReportSearchResultDTO
    {
        public int Id { get; set; }
        public int ExaminationId { get; set; }
        public int ExaminerId { get; set; }
        public int ExamineeId { get; set; }
        public string ExamineeName { get; set; }
        public string ExamineeEmail { get; set; }
        public string ExaminerName { get; set; }
        public string ExaminerEmail { get; set; }
        public int NumberOfPrescriptions { get; set; }
        public DateTime WrittenOn { get; set; }
    }

    public class ReportSearchResultSingleDTO : ReportSearchResultDTO
    {
        public string Text { get; set; }
        public IEnumerable<int> PrescriptionIds { get; set; }
    }
}
