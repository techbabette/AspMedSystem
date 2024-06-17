using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class AuditLogSearchDTO : PagedSearch
    {
        public string? UserEmail { get; set; }
        public string? UseCaseName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }

    public class AuditLogSearchResultDTO
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string UseCaseName { get; set;}
        public DateTime CreatedAt { get; set; }
    }

    public class AuditLogSearchSingleResultDTO : AuditLogSearchResultDTO
    {
        public string Data { get; set; }
    }

}
