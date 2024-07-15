using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Repository.IRepository;

namespace MagicVilla_API.Repository {
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository {
        private readonly ApplicationDbContext _db;

        public VillaNumberRepository(ApplicationDbContext db) : base(db) {
            this._db = db;
        }

        public async Task<VillaNumber> UpdateAsync(VillaNumber entity) {
            _db.VillaNumber.Update(entity);
            await SaveAsync();
            return entity;
        }
    }
}
