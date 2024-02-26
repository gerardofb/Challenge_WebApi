using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IGenericElasticRepository<TEntity> where TEntity : class
    { 
        public Task<TEntity> InsertPriorPermissions(TEntity priorPermission);
        public Task<List<TEntity>> InsertBulkPriorPermissions(List<TEntity> priorPermissions, CancellationToken token);
        public List<TEntity> GetPermissionsOrderedByDateUpdated(string sortField, string dateInitial);
    }
}
