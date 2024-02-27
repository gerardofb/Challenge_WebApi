using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interfaces;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;
using Confluent.Kafka;

namespace Repository.Implementation
{
    public class RepositoryPermission<TEntity> : GenericRepository<PermissionsEmployee>
    {
        
        public RepositoryPermission(ChallengeContext contexto) : base(contexto)
        {
           
        }
        public override void Insert(PermissionsEmployee permission)
        {
            if (!set.Where(d => d.Employees.Any(r => r == permission.Employees[0])).Select(a => a.PermissionTypes).Any(d => d.Name == permission.PermissionTypes.Name))
            {
                set.Add(permission);
                ProduceMessage(InsertOp);
            }
            else throw new System.NotImplementedException();
        }
        public override async void ProduceMessage(string Message)
        {
            using (var producer = new ProducerBuilder<Null, string>(configKafka).Build())
            {
                await producer.ProduceAsync("logKafka", new Message<Null, string> { Value = String.Format("{0}/{1}", Guid.NewGuid().ToString("D"), Message) });
            }
        }
    }
}
