using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityType, int id) :
            base($"Entity of type '{entityType}' with an id of {id} doesn't exist.")
        {
        }
    }

    public class SubEntityNotFoundException : EntityNotFoundException
    {
        public string entity;
        public int id;
        public SubEntityNotFoundException(string entityType, int id) : base(entityType, id)
        {
            entity = entityType;
            this.id = id;
        }
    }
}
