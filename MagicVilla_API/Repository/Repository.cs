using MagicVilla_API.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using MagicVilla_API.Repository.IRepository;

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

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null) {
            IQueryable<T> query = _dbSet;
            if (filter != null) {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true) {
            IQueryable<T> query = _dbSet;
            if (tracked == false) {
                query.AsNoTracking();
            }
            if (filter != null) {
                query = query.Where(filter);
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
