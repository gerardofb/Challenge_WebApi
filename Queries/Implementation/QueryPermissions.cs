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

        public List<MaterializedViewPermissions> Get(int employeeId)
        {
            List<MaterializedViewPermissions> salida = null;
            Employee employeefound = context.Employees.Find(employeeId);
            if (employeefound != null)
            {
                salida = context.MaterializedViews.Where(a => a.UserName == employeefound.Name && a.LastName == employeefound.LastName).ToList();
                if (salida != null && salida.Count == 0)
                {
                    salida = null;
                }
            }
            return salida;
        }

        public List<PermissionsEmployee> GetPermissionsExplicit(int employeeId)
        {
            List<PermissionsEmployee> salida = null;
            try
            {
                salida = context.Permissions.Where(a => a.Employees.Any(r => r.Id == employeeId)).ToList();
                if (salida.Count > 0)
                {
                    salida.ForEach(r => r.PermissionTypes = context.PermissionsTypes.Find(r.PermissionTypeId));
                }
                else
                {
                    salida = null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return salida;
        }

        public List<Employee> Get(string NameEmployee)
        {
            List<Employee> employees = null;
            using (context)
            {
                try
                {
                    var salida = context.Employees.Where(a => a.Name == NameEmployee.Trim() || a.LastName == NameEmployee.Trim()).ToList();
                    if (salida.Count > 0)
                    {
                        salida.ForEach(r => r.WorkArea = context.WorkAreas.Where(a => a.Employee.Contains(r)).ToList());
                        employees = salida;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            return employees;
        }

        Employee IQueryPermissions.Get(Employee employee)
        {
            return context.Employees.Find(employee.Id);
        }

        public PermissionType Get(PermissionsEmployee permission)
        {
            return context.PermissionsTypes.Find(permission.PermissionTypeId);
        }
    }
}