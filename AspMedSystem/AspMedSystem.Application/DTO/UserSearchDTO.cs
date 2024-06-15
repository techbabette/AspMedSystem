using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class UserSearchDTO : PagedSearch
    {
        public string? Keyword { get; set; }
    }

    public class UserSearchResultDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Group { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
