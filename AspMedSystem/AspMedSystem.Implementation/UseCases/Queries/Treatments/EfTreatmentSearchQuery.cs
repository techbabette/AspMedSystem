using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Queries.Treatments;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Treatments
{
    public class EfTreatmentSearchQuery : EfUseCase, ITreatmentSearchQuery
    {
        private EfTreatmentSearchQuery()
        {
        }

        public EfTreatmentSearchQuery(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Search treatments";

        public PagedResponse<TreatmentSearchResultDTO> Execute(TreatmentSearchDTO search)
        {
            var query = Context.Treatments.AsQueryable();

            if (!search.Name.IsNullOrEmpty())
            {
                query = query.Where(treatment => treatment.Name.ToLower().Contains(treatment.Name.ToLower()));
            }

            if (search.DateFrom.HasValue)
            {
                query = query.Where(treatment => treatment.CreatedAt >= search.DateFrom.Value);
            }

            if (search.DateTo.HasValue)
            {
                query = query.Where(treatment => treatment.CreatedAt <= search.DateTo.Value);
            }

            return query.AsPagedResponse(search, treatment => new TreatmentSearchResultDTO
            {
                Id = treatment.Id,
                Name = treatment.Name,
                CreatedAt = treatment.CreatedAt
            });
        }
    }
}
