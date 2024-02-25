using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;

namespace Repository.Implementation
{
    public class RepositoryEmployee : IRepositoryEmployee
    {
        private bool disposed = false;
        private ChallengeContext context;
        public RepositoryEmployee(ChallengeContext context)
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

        public void InsertEmployee<TEmployee>(Employee employee) where TEmployee : class
        {
            context.Employees.Add(employee);
        }

        public void DeleteEmployee<TEmployee>(Employee employee) where TEmployee : class
        {
            Employee deleted = context.Employees.Find(employee.Id);
            context.Employees.Remove(deleted);
        }

        public void UpdateEmployee<TEmployee>(Employee employee) where TEmployee : class
        {
            context.Entry(employee).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
