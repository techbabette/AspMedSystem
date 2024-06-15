using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class UserSearchSingleDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Group { get; set; }
        public IEnumerable<string> GroupAllowedUseCases { get; set; }
        public IEnumerable<string> UserAllowedUseCases { get; set; }
        public IEnumerable<string> UserDisallowedUseCases { get; set; }
    }
}
