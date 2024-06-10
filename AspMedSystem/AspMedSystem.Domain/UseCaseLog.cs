using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Domain
{
    public class UseCaseLog : Entity
    {
        public string UseCaseName { get; set; }

        public string ActorEmail { get; set; }

        [Column(TypeName = "text")]
        public string Data { get; set; }
    }
}
