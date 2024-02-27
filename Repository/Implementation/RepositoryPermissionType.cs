using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;
using Repository.Interfaces;
using Confluent.Kafka;


namespace Repository.Implementation
{
    public class RepositoryPermissionType<TEntity> : GenericRepository<PermissionType>
    {
        public static void handler(DeliveryReport<Null, string> delivery)
        {
            System.Diagnostics.Debug.WriteLine(String.Format("Salida del handler {0} {1} {3}", delivery.Error, delivery.Topic, delivery.Value));
        }
        public RepositoryPermissionType(ChallengeContext contexto) : base(contexto)
        {
        }

        public override void Update(PermissionType permissionType)
        {
            PermissionType permissionModified = set.Find(permissionType.Id);
            if (permissionModified != null)
                context.Entry(permissionType).State = EntityState.Modified;
            else context.Entry(permissionType).State = EntityState.Unchanged;
            ProduceMessage(ModifyOp);
        }
        public async void ProduceMessage(string Message)
        {
            using (var producer = new ProducerBuilder<Null, string>(configKafka).Build())
            {
                await producer.ProduceAsync("logKafka", new Message<Null, string> { Value = String.Format("{0}/{1}", Guid.NewGuid().ToString("D"), Message) });
            }
        }
    }
}

