﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;

namespace Queries.Interfaces
{
    public interface IQueryPermissions : IDisposable
    {
        public Employee Get(Employee employee);
        public List<MaterializedViewPermissions> Get(int employeeId);
        public List<Employee> Get(string NameEmployee);
        public PermissionType Get(PermissionsEmployee permission);
        public List<PermissionsEmployee> GetPermissionsExplicit(int employeeId);
    }
}
