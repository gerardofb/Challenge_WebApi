using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;
using Repository.Interfaces;



    namespace Repository.Implementation
    {
        public class RepositoryPermissionType : IPermissionTypeRepository
        {
            private bool disposed = false;
            private ChallengeContext context;
            public RepositoryPermissionType(ChallengeContext context)
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

            public void InsertPermissionType<TPermissionType>(PermissionType permissionType) where TPermissionType : class
            {
                context.PermissionsTypes.Add(permissionType);
            }

            public void DeletePermissionType<TPermissionType>(PermissionType permissionType) where TPermissionType : class
            {
                PermissionType deleted = context.PermissionsTypes.Find(permissionType.Id);
                context.PermissionsTypes.Remove(deleted);
            }

            public void UpdatePermissionType<TPermissionType>(PermissionType permissionType) where TPermissionType : class
            {
                context.Entry(permissionType).State = EntityState.Modified;
            }

            public void Save()
            {
                context.SaveChanges();
            }
        }
    }

