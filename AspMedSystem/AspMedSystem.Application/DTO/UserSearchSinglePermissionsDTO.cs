using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class UserSearchSinglePermissionsDTO
    {
        public int Id { get; set; }
        public string Group { get; set; }
        public IEnumerable<string> GroupAllowedPermissions { get; set;}
        public IEnumerable<string> UserAllowedPermissions { get; set; }
        public IEnumerable<string> UserDisallowedPermissions { get; set; }
    }
}
