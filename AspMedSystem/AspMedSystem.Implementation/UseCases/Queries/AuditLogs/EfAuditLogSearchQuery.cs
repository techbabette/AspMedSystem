using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Queries.AuditLogs;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.AuditLogs
{
    public class EfAuditLogSearchQuery : EfUseCase, IAuditLogSearchQuery
    {
        private EfAuditLogSearchQuery()
        {
        }

        public EfAuditLogSearchQuery(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Search audit logs";

        public PagedResponse<AuditLogSearchResultDTO> Execute(AuditLogSearchDTO search)
        {
            var query = Context.UseCaseLogs.AsQueryable();

            if (!search.UserEmail.IsNullOrEmpty())
            {
                query = query.Where(log => log.ActorEmail.ToLower().Contains(search.UserEmail.ToLower()));
            }

            if (!search.UseCaseName.IsNullOrEmpty())
            {
                query = query.Where(log => log.UseCaseName.ToLower().Contains(search.UseCaseName.ToLower()));
            }

            if (search.DateFrom.HasValue)
            {
                query = query.Where(log => log.CreatedAt >= search.DateFrom.Value);
            }

            if (search.DateTo.HasValue)
            {
                query = query.Where(log => log.CreatedAt <= search.DateTo.Value);
            }

            query = query.OrderByDescending(log => log.CreatedAt);

            return query.AsPagedResponse(search, log => new AuditLogSearchResultDTO{
                Id = log.Id,
                UserEmail = log.ActorEmail,
                UseCaseName = log.UseCaseName,
                CreatedAt = log.CreatedAt
            });
        }
    }
}
