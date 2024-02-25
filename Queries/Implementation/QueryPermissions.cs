using Queries.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Models;

namespace Queries.Implementation
{
    public class QueryPermissions : IQueryPermissions
    {
        private bool disposed = false;
        private ChallengeContext context;
        public QueryPermissions(ChallengeContext context)
        {
            this.context = context;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Employee Get(Employee employee)
        {
            return context.Employees.Find(employee.Id);
        }

        public List<Employee> Get(string NameEmployee)
        {
            List<Employee> employees = null;
            using (context)
            {
                var salida = context.Employees.Where(a => a.Name == NameEmployee.Trim() || a.LastName == NameEmployee.Trim()).ToList();
                if (salida != null && salida.Count > 0)
                    employees = salida;
            }
            return employees;
        }
    }
}