using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class TreatmentSearchDTO : PagedSearch
    {
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }

    public class TreatmentSearchResultDTO
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TreatmentSearchResultSingleDTO : TreatmentSearchResultDTO
    {
        public int NumberOfTimesPrescribed { get; set; }
    }
}
