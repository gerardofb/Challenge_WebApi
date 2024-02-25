using System;
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
        public List<Employee> Get(string NameEmployee);
    }
}
