﻿using AspMedSystem.Application;
using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Queries.Reports;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Reports
{
    public class EfReportSearchExamineeQuery : EfUseCase, IReportSearchExamineeQuery
    {
        private readonly IApplicationActor actor;

        private EfReportSearchExamineeQuery()
        {
        }

        public EfReportSearchExamineeQuery(MedSystemContext context, IApplicationActor actor) : base(context)
        {
            this.actor = actor;
        }

        public string Name => "Search reports as examinee";

        public PagedResponse<ReportSearchResultDTO> Execute(ReportSearchDTO search)
        {
            var query = Context.Reports.AsQueryable();

            query = query.Where(report => report.Examination.ExamineeId == actor.Id);

            if (search.ExaminationId.HasValue)
            {
                query = query.Where(report => report.ExaminationId == search.ExaminationId.Value);
            }

            if (search.ExaminerId.HasValue)
            {
                query = query.Where(report => report.Examination.ExaminationTerm.ExaminerId == search.ExaminerId.Value);
            }

            if (!search.ExaminerKeyword.IsNullOrEmpty())
            {
                query = query.Where(report =>
                (report.Examination.ExaminationTerm.Examiner.FirstName + " " + report.Examination.ExaminationTerm.Examiner.LastName).ToLower().Contains(search.ExaminerKeyword.ToLower()) ||
                report.Examination.ExaminationTerm.Examiner.Email.ToLower().Contains(search.ExaminerKeyword.ToLower())
                );
            }

            if (search.DateFrom.HasValue)
            {
                query = query.Where(report => report.CreatedAt >= search.DateFrom.Value);
            }

            if (search.DateTo.HasValue)
            {
                query = query.Where(report => report.CreatedAt <= search.DateTo.Value);
            }

            query = query.OrderByDescending(report => report.CreatedAt);
            return query.AsPagedResponse(search, report => new ReportSearchResultDTO
            {
                Id = report.Id,
                ExaminationId = report.ExaminationId,
                ExamineeId = report.Examination.ExamineeId,
                ExaminerId = report.Examination.ExaminationTerm.ExaminerId,
                ExamineeEmail = report.Examination.Examinee.Email,
                ExaminerEmail = report.Examination.ExaminationTerm.Examiner.Email,
                ExamineeName = report.Examination.Examinee.FirstName + " " + report.Examination.Examinee.LastName,
                ExaminerName = report.Examination.ExaminationTerm.Examiner.FirstName + " " + report.Examination.ExaminationTerm.Examiner.LastName,
                NumberOfPrescriptions = report.UserTreatments.Count,
                WrittenOn = report.CreatedAt
            });
        }
    }
}
