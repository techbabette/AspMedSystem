using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class GroupSearchSingleResultDTO
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool DefaultRegister { get; set; }
        public IEnumerable<string> Users { get; set; }
        public IEnumerable<string> AllowedUseCases { get; set; }
        public IEnumerable<string> DisallowedUseCases { get; set; }
    }
}
