using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class UserTreatmentSearchDTO : PagedSearch
    {
        public int? ReportId { get; set; }
        public int? TreatmentId { get; set; }
        public int? PresciberId { get; set; }
        public int? PrescribeeId { get; set; }
        public string? PrescriberKeyword { get; set; }
        public string? PrescribeeKeyword { get; set; }
        public string? TreatmentKeyword { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    public class UserTreatmentSearchResultDTO
    {
        public int ReportId { get; set; }
        public int TreatmentId { get; set; }
        public int PrescriberId { get; set; }
        public int PrescribeeId { get; set; }
        public string PrescribeeEmail { get; set; }
        public string PrescribeeName { get; set; }
        public string Treatment { get; set; }
        public string PrescriberEmail { get; set; }
        public string PrescriberName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class UserTreatmentSearchSingleResult : UserTreatmentSearchResultDTO
    {
        public string? Note { get; set; }
    }
}
