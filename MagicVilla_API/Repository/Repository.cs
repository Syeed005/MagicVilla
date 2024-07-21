using MagicVilla_API.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using MagicVilla_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MagicVilla_API.Repository {
    public class Repository<T> : IRepository<T> where T:class {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db) {
            this._db = db;
            this._dbSet = db.Set<T>();
        }
        public async Task CreateAsync(T entity) {
            await _dbSet.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includePropertise = null, int pageSize = 0, int pageNumber = 1) {
            IQueryable<T> query = _dbSet;
            if (filter != null) {
                query = query.Where(filter);
            }
            if (pageSize > 0) {
                if (pageSize > 100) {
                    pageSize = 100;
                }
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }
            if (includePropertise != null) {
                foreach (var includeProp in includePropertise.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includePropertise = null) {
            IQueryable<T> query = _dbSet;
            if (tracked == false) {
                query.AsNoTracking();
            }
            if (filter != null) {
                query = query.Where(filter);
            }
            if (includePropertise != null) {
                foreach (var includeProp in includePropertise.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(T entity) {
            _dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync() {
            await _db.SaveChangesAsync();
        }
    }
}
