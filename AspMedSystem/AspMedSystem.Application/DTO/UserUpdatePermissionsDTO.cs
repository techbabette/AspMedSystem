using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class UserUpdatePermissionsDTO
    {
        public int Id { get; set; }
        public IEnumerable<string> AllowedUseCases { get; set; }
        public IEnumerable<string> DisallowedUseCases { get; set; }
    }
}
