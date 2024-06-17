using AspMedSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.UseCases.Queries.AuditLogs
{
    public interface IAuditLogSearchQuery : IQuery<PagedResponse<AuditLogSearchResultDTO>, AuditLogSearchDTO> 
    {
    }
}
