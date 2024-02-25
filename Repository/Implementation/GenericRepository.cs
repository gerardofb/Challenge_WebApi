using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        internal DbContext context;
        internal DbSet<TEntity> set;
        public GenericRepository(ChallengeContext contexto)
        {
            context = contexto;
            this.set = context.Set<TEntity>();
        }
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity,bool>>filter = null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null,string includeProperties = "")
        {
            IQueryable<TEntity> query = set;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            foreach(var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (orderBy != null)
            {
                return orderBy(query);
            }
            else return query.ToList();
        } 
        public virtual TEntity GetById(object id)
        {
            return set.Find(id);
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
            if(context.Entry(deleted).State == EntityState.Detached)
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
    }
}
