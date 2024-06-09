using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Domain
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public DateTime BirthDate { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public virtual ICollection<UserPermission> UserPermissions { get; set; } = new HashSet<UserPermission>();
        public virtual ICollection<ExaminationTerm> ExaminationTerms { get; set; } = new HashSet<ExaminationTerm>();

        public virtual ICollection<Examination> Examinations { get; set; } = new HashSet<Examination>();
    }
}
