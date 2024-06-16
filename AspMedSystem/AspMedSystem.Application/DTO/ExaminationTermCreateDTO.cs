using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class ExaminationTermCreateDTO
    {
        public int ExaminerId { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class ExaminationTermUpdateDTO : ExaminationTermCreateDTO
    {
        public int Id { get; set; }
    }
}
