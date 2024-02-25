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
    public class RepositoryPermissionType<TEntity> : GenericRepository<PermissionType>
    {
        public RepositoryPermissionType(ChallengeContext contexto) : base(contexto)
        {
        }

        public override void Update(PermissionType permissionType)
        {
            PermissionType permissionModified = set.Find(permissionType.Id);
            if (permissionModified != null)
                context.Entry(permissionType).State = EntityState.Modified;
            else context.Entry(permissionType).State = EntityState.Unchanged;
        }
    }
}

