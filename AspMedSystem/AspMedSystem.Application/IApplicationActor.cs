using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application
{
    public interface IApplicationActor
    {
        int Id { get; }
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
        DateTime BirthDate { get; }
        IEnumerable<string> AllowedUseCases { get; }
    }
}
