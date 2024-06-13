using AspMedSystem.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Implementation
{
    public class UnauthorizedActor : IApplicationActor
    {
        public int Id => 0;

        public string Email => "/";

        public string FirstName => "unauthorized";

        public string LastName => "unauthorized";

        public DateTime BirthDate => DateTime.UtcNow;

        public IEnumerable<string> AllowedUseCases => new List<string> { };
    }
}
