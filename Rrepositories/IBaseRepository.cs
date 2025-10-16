using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBaseRepository<T>
    {
        public T Add(T entity);
        public T Update(T entity);
        public T Delete(T entity);
        public Task SaveChanges();
        public Task<T?> FindOne(Expression<Func<T, bool>> func);
        public Task<List<T>?> Find(Expression<Func<T, bool>> func);
    }
}
