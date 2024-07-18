using System.Linq.Expressions;

namespace MagicVilla_API.Repository.IRepository {
    public interface IRepository<T> where T:class {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includePropertise = null);
        Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includePropertise = null);
        Task CreateAsync(T entity);
        Task RemoveAsync(T entity);
        Task SaveAsync();
    }
}
