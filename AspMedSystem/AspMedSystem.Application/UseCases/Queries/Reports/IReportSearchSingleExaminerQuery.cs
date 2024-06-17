using AspMedSystem.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.UseCases.Queries.Reports
{
    public interface IReportSearchSingleExaminerQuery : IQuery<ReportSearchResultSingleDTO, int>
    {
    }
}
