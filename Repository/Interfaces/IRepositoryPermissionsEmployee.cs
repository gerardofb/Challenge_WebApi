using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;

namespace Repository.Interfaces
{
    public interface IRepositoryPermissionsEmployee : IDisposable
    {

        void InsertPermission<TPermission>(PermissionsEmployee permission) where TPermission : class;
        void DeletePermission<TPermission>(PermissionsEmployee permission) where TPermission : class;
        void UpdatePermission<TPermission>(PermissionsEmployee permission) where TPermission : class;
        void Save();
    }
}
