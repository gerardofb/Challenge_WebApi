using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Interfaces
{
    public interface IQueryElasticPermissions<TEntity> where TEntity : class
    { 
        public List<TEntity>GetPermissionsOrderedByDateUpdated(string sortField, string dateInitial);
    }
}
