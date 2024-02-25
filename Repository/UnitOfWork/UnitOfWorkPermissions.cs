using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;
using Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.UnitOfWork
{
    public class UnitOfWorkPermissions : IDisposable
    {
        private bool disposed = false;
        private ChallengeContext context;
        private RepositoryEmployee<Employee> employeeRepository;
        private RepositoryPermissionType<PermissionType> permissionTypeRepository;
        private RepositoryPermission<PermissionsEmployee> permissionsEmployeeRepository;
        public UnitOfWorkPermissions(ChallengeContext contexto)
        { 
            context = contexto;
        }
        public GenericRepository<Employee> EmployeeRepository
        {
            get
            {
                if (this.employeeRepository == null)
                {
                    this.employeeRepository = new RepositoryEmployee<Employee>(context);
                }
                return this.employeeRepository;
            }
        }
        public GenericRepository<PermissionsEmployee> PermissionRepository
        {
            get
            {
                if (this.permissionsEmployeeRepository == null)
                {
                    this.permissionsEmployeeRepository = new RepositoryPermission<PermissionsEmployee>(context);
                }
                return this.permissionsEmployeeRepository;
            }
        }
        public GenericRepository<PermissionType> PermissionTypeRepository
        {
            get
            {
                if (this.permissionTypeRepository == null)
                {
                    this.permissionTypeRepository = new RepositoryPermissionType<PermissionType>(context);
                }
                return this.permissionTypeRepository;
            }
        }
        public void Save()
        {
            context.SaveChanges();
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
    }
}
