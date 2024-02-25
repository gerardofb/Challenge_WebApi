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
    public class RepositoryEmployee<TEntity> : GenericRepository<Employee>
    {
        public RepositoryEmployee(ChallengeContext contexto) : base(contexto)
        {
        }
    }
}
