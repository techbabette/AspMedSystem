using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class ExaminerSearchDTO : PagedSearch
    {
        public string? Keyword { get; set; }
        public bool? Available { get; set; }
    }

    public class ExaminerSearchResultDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int AvailableTermsCount { get; set; }
    }

    public class ExaminerSearchResultSingleDTO : ExaminerSearchResultDTO
    {
        public IEnumerable<int> AvailableTerms { get; set; }
    }
}
