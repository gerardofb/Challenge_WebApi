using Queries.Interfaces;
using Infrastructure.Contexts;
using Infrastructure.Models;
using Confluent.Kafka;
using System.Net;

namespace Queries.Implementation
{
    public class QueryPermissions : IQueryPermissions
    {
        private bool disposed = false;
        private ChallengeContext context;
        private ProducerConfig configKafka;
        private const string ModifyOp = "Modify";
        private const string InsertOp = "Request";
        private const string GetOp = "Get";
        public QueryPermissions(ChallengeContext context)
        {
            this.context = context;
            configKafka = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
            };
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

        public async void ProduceMessage(string Message)
        {
            using (var producer = new ProducerBuilder<Null, string>(configKafka).Build())
            {
                await producer.ProduceAsync("logKafka", new Message<Null, string> { Value = String.Format("{0}/{1}", Guid.NewGuid().ToString("D"), Message) });
            }
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
                else
                {
                    ProduceMessage(GetOp);
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
                    ProduceMessage(GetOp);
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
                        ProduceMessage(GetOp);
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
            ProduceMessage(GetOp);
            return context.Employees.Find(employee.Id);
            
        }

        public PermissionType Get(PermissionsEmployee permission)
        {
            ProduceMessage(GetOp);
            return context.PermissionsTypes.Find(permission.PermissionTypeId);
        }


    }
}