using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OpenCMS.Application.Interfaces.Repository;
using OpenCMS.Application.Interfaces.Services;
using OpenCMS.Domain.Entities;

namespace OpenCMS.Application.Repository
{
    public class RepositoryService<T, TKey> : IRepository<T, TKey> where T : BaseEntity<TKey>
    {
        private readonly OpenCMSDb _db;
        private readonly ITenantService _tenantService;
        private readonly string _userId;
        private readonly int _tenantId;

        public RepositoryService(OpenCMSDb db,ITenantService tenantService, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _tenantService = tenantService;
            _userId = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            _tenantId = tenantService.GetTenant();
        }

        public IQueryable<T> Fetch(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            var tenant = _tenantService.GetTenant();
            IQueryable<T> src = _db.Set<T>().Where(x => x.TenantId == _tenantId && x.Deleted == false);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var i in includeProperties.Split(","))
                {
                    src = src.Include(i);
                }
            }
            if (filter != null)
                return src.Where(filter);
            return src;
        }

        public List<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> res = _db.Set<T>().Where(x => x.TenantId == _tenantId && x.Deleted == false); 
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
            var _dbSet = _db.Set<T>().AsQueryable();
            if (!string.IsNullOrEmpty(includeProperties))
            {
                
                foreach (var i in includeProperties.Split(','))
                {
                    _dbSet = _dbSet.Include(i);
                }

              }
            return _dbSet.Where(filter).FirstOrDefault();
        }

        public T Find(TKey id)
        {
            return _db.Set<T>().Find(id);
        }

        public async Task<T> Insert(T item)
        {
            item.TenantId = _tenantId;
            item.Deleted = false;
            _db.Add(item);
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task<T> Update(T item)
        {
            item.TenantId = _tenantId;
            _db.Add(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _db.SaveChangesAsync();
            return item;
        }

        public async Task Delete(TKey id)
        {
            var item = Find(id);
            item.Deleted = true;
            await _db.SaveChangesAsync();
        }
    }
}
