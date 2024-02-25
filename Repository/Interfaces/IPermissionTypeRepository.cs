using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;

namespace Repository.Interfaces
{
    public interface IPermissionTypeRepository : IDisposable
    {

        void InsertPermissionType<TPermissionType>(PermissionType permissionType) where TPermissionType : class;
        void DeletePermissionType<TPermissionType>(PermissionType permissionType) where TPermissionType : class;
        void UpdatePermissionType<TPermissionType>(PermissionType permissionType) where TPermissionType : class;
        void Save();
    }
}
