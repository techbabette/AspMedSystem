using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class GroupSearchDTO : PagedSearch
    {
        public string? Name { get; set; }
        public bool? DefaultRegister { get; set; }
    }

    public class GroupSearchResultDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool DefaultRegister { get; set; }
        public int NumberOfUsers { get; set; }
        public int NumberOfPermissionSets { get; set; }
    }
}
