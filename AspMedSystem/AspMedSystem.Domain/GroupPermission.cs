﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Domain
{
    public class GroupPermission : PermissionEffect
    {
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
    }
}
