using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
namespace Repository.Implementation
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal ProducerConfig configKafka;
        internal DbContext context;
        internal DbSet<TEntity> set;
        internal const string ModifyOp = "Modify";
        internal const string InsertOp = "Request";
        internal const string GetOp = "Get";
        public GenericRepository(ChallengeContext contexto)
        {
            context = contexto;
            this.set = context.Set<TEntity>();
            configKafka = new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
            };
        }
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = set;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                //ProduceMessage(GetOp);
                return orderBy(query);
            }
            else
            {
                //ProduceMessage(GetOp);
                return query.ToList();
            }
        }
        public virtual TEntity GetById(object id)
        {
            var salida = set.Find(id);
            if (salida != null)
            {
                //ProduceMessage(GetOp);
            }
            return salida;

        }
        public virtual void Insert(TEntity entity)
        {
            set.Add(entity);
        }
        public virtual void Delete(object id)
        {
            TEntity deleted = set.Find(id);
            Delete(deleted);
        }
        public virtual void Delete(TEntity deleted)
        {
            if (context.Entry(deleted).State == EntityState.Detached)
            {
                set.Attach(deleted);
            }
            set.Remove(deleted);
        }
        public virtual void Update(TEntity updated)
        {
            set.Attach(updated);
            context.Entry(updated).State = EntityState.Modified;
        }
        public virtual async void ProduceMessage(string Message)
        {
            using (var producer = new ProducerBuilder<Null, string>(configKafka).Build())
            {
                await producer.ProduceAsync("logKafka", new Message<Null, string> { Value = String.Format("{0}/{1}", Guid.NewGuid().ToString("D"), Message) });
            }
        }
    }
}
