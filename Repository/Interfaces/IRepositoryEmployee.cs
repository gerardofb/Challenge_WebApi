using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Models;


namespace Repository.Interfaces
{
    public interface IRepositoryEmployee : IDisposable
    {
        void InsertEmployee<TEmployee>(Employee employee) where TEmployee : class;
        void DeleteEmployee<TEmployee>(Employee employee) where TEmployee : class;
        void UpdateEmployee<TEmployee>(Employee employee) where TEmployee:class;
        void Save();
    }
}
