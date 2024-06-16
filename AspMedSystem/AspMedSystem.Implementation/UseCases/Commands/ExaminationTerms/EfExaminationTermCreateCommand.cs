using AspMedSystem.Application.DTO;
using AspMedSystem.Application.UseCases.Commands.ExaminationTerms;
using AspMedSystem.DataAccess;
using AspMedSystem.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases.Commands.ExaminationTerms
{
    public class EfExaminationTermCreateCommand : EfUseCase, IExaminationTermCreateCommand
    {
        private readonly ExaminationTermCreateValidator validator;

        private EfExaminationTermCreateCommand()
        {
        }

        public EfExaminationTermCreateCommand(MedSystemContext context, ExaminationTermCreateValidator validator) : base(context)
        {
            this.validator = validator;
        }

        public string Name => "Publish examination term";

        public void Execute(ExaminationTermCreateDTO data)
        {
            validator.ValidateAndThrow(data);

            Context.ExaminationTerms.Add(new Domain.ExaminationTerm
            {
                Date = data.DateTime,
                ExaminerId = data.ExaminerId,
            });

            Context.SaveChanges();
        }
    }
}
