using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IBaseRepository<T>
    {
        public T Add(T entity);
        public T Update(T entity);
        public T Delete(T entity);
        public IQueryable<T> Query();
        public Task SaveChanges();
    }
}
