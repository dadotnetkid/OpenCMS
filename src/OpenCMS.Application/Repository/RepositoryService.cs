using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Domain.Entities;

namespace OpenCMS.Application.Repository
{
    public class RepositoryService<T, TKey> : IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        private readonly OpenCMSDb _db;

        public RepositoryService(OpenCMSDb db)
        {
            _db = db;
        }

        public IQueryable<T> Fetch(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
                return _db.Set<T>().Where(filter);
            return _db.Set<T>();
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> res = _db.Set<T>();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var i in includeProperties.Split(","))
                {
                    res = res.Include(i);
                }
            }
            if (filter != null)
                return res.Where(filter).ToList();
            return res.ToList();
        }

        public T Find(Expression<Func<T, bool>> filter, string includeProperties = "")
        {
            if (!string.IsNullOrEmpty(includeProperties))
            {
                var _dbSet = _db.Set<T>().AsQueryable();
                foreach (var i in includeProperties.Split(','))
                {
                    _dbSet = _dbSet.Include(i);
                }

                return _dbSet.AsNoTracking().FirstOrDefault();
            }
            return _db.Set<T>().AsNoTracking().Where(filter).FirstOrDefault();
        }

        public T Find(TKey id)
        {
            return _db.Set<T>().Find(id);
        }

        public async Task<T> Insert(T item)
        {
            _db.Add(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<T> Update(T item)
        {
            _db.Add(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task Delete(TKey id)
        {
            var item = Find(id);
            _db.Remove(item);
            await _db.SaveChangesAsync();
        }
    }
}
