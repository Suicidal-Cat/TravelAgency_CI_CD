using DataBase;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public T Add(T entity)
        {
            var entry = _dbSet.Add(entity);
            return entry.Entity;
        }

        public T Update(T entity)
        {
            var entry = _dbSet.Update(entity);
            return entry.Entity;
        }

        public T Delete(T entity)
        {
            var entry = _dbSet.Remove(entity);
            return entry.Entity;
        }

        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
        private IQueryable<T> Query() => _dbSet.AsQueryable();

        public async Task<T?> FindOne(Expression<Func<T, bool>> func)
        {
            return await Query().FirstOrDefaultAsync(func);
        }

        public async Task<List<T>?> Find(Expression<Func<T, bool>> func)
        {
            return await Query().Where(func).ToListAsync();
        }
    }
}
