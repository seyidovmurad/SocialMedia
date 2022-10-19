using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext>
        : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task Add(TEntity entity)
        {
            using(var context = new TContext())
            {
                var newEntity = context.Entry(entity);
                newEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var newEntity = context.Entry(entity);
                newEntity.State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task<TEntity?> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                var entity = await context.Set<TEntity>().SingleOrDefaultAsync(filter);
                return entity;
            }
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context =new TContext())
            {
                var entities = filter==null
                    ? await context.Set<TEntity>().ToListAsync()
                    : await context.Set<TEntity>().Where(filter).ToListAsync();
                return entities;
            }
        }

        public async Task Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var newEntity = context.Entry(entity);
                newEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
