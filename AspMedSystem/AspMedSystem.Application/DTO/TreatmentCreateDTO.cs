using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.DTO
{
    public class TreatmentCreateDTO
    {
        public string Name { get; set; }
        public bool Prescribable { get; set; }
    }

    public class TreatmentUpdateDTO : TreatmentCreateDTO
    {
        public int Id { get; set; }
    }
}
