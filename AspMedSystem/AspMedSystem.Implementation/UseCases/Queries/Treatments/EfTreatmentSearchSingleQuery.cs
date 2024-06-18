using AspMedSystem.Application.DTO;
using AspMedSystem.Application.Exceptions;
using AspMedSystem.Application.UseCases.Queries.Treatments;
using AspMedSystem.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Queries.Treatments
{
    public class EfTreatmentSearchSingleQuery : EfUseCase, ITreatmentSearchSingleQuery
    {
        private EfTreatmentSearchSingleQuery()
        {
        }

        public EfTreatmentSearchSingleQuery(MedSystemContext context) : base(context)
        {
        }

        public string Name => "Show treatment";

        public TreatmentSearchResultSingleDTO Execute(int search)
        {
            var treatment = Context.Treatments.Where(treatment => treatment.Id == search)
                                              .Select(treatment => new TreatmentSearchResultSingleDTO
                                              {
                                                  Id = treatment.Id,
                                                  Name = treatment.Name,
                                                  NumberOfTimesPrescribed = treatment.UserTreatments.Count,
                                                  CreatedAt = treatment.CreatedAt,
                                                  Prescribable = treatment.Prescribable
                                              })
                                              .FirstOrDefault();

            if(treatment == null)
            {
                throw new EntityNotFoundException("Treatment", search);
            }

            return treatment;
        }
    }
}
