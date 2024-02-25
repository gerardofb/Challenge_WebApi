﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class PermissionType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual IEnumerable<Employee>? Employees { get; set; }
        public virtual IEnumerable<PermissionsEmployee>? PermisssionsEmployees { get; set; }

    }
}
