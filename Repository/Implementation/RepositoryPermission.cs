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
    public class RepositoryPermission : IRepositoryPermissionsEmployee
    {
        private bool disposed = false;
        private ChallengeContext context;
        public RepositoryPermission(ChallengeContext context)
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

        public void InsertPermission<TPermission>(PermissionsEmployee permission) where TPermission : class
        {
            context.Permissions.Add(permission);
        }

        public void DeletePermission<TPermission>(PermissionsEmployee permission) where TPermission : class
        {
            PermissionsEmployee deleted = context.Permissions.Find(permission.Id);
            context.Permissions.Remove(deleted);
        }

        public void UpdatePermission<TPermission>(PermissionsEmployee permission) where TPermission : class
        {
            context.Entry(permission).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
