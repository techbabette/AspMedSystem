﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspMedSystem.Domain
{
    public class UserPermission : PermissionEffect
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
