using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OpenCMS.Domain.Entities;

namespace OpenCMS.Application.Interfaces.Repository
{
    public interface IRepository<T, TKey> where T : BaseEntity<TKey>
    
    {
        public IQueryable<T> Fetch(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        public List<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties=null);
        T Find(Expression<Func<T, bool>> filter,string includeProperties="");
        public T Find(TKey id);
        Task<T> Insert(T item);
        Task<T> Update(T item);
        Task Delete(TKey id);
    }
}
