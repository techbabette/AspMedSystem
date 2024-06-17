using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Queries.AuditLogs;
using AspMedSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.AuditLogs
{
    public class EfAuditLogSearchSingleQuery : EfUseCase, IAuditLogSearchSingleQuery
    {
        private EfAuditLogSearchSingleQuery()
        {
        }

        public EfAuditLogSearchSingleQuery(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Show audit log";

        public AuditLogSearchSingleResultDTO Execute(int search)
        {
            var auditLog = Context.UseCaseLogs.Where(log => log.Id == search)
                                               .Select(log => new AuditLogSearchSingleResultDTO
                                               {
                                                   Id = log.Id,
                                                   UserEmail = log.ActorEmail,
                                                   UseCaseName = log.UseCaseName,
                                                   CreatedAt = log.CreatedAt,
                                                   Data = log.Data
                                               })
                                               .FirstOrDefault();

            if (auditLog == null)
            {
                throw new EntityNotFoundException("Audit log", search);
            }

            return auditLog;
        }
    }
}
