using AspMedSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation.UseCases
{
    public abstract class EfUseCase
    {
        private readonly MedSystemContext _context;

        protected EfUseCase(MedSystemContext context)
        {
            _context = context;
        }

        protected MedSystemContext Context => _context;
    }
}
