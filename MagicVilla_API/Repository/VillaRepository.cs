using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_API.Repository {
    public class VillaRepository : Repository<Villa>, IVillaRepository {
        private readonly ApplicationDbContext _db;

        public VillaRepository(ApplicationDbContext db) : base(db) {
            this._db = db;
        }
        
        public async Task<Villa> UpdateAsync(Villa entity) {
            _db.Villas.Update(entity);
            await SaveAsync();
            return entity;
        }
    }
}
