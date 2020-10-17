using System.Threading.Tasks;

namespace Persistence.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(T entity);
        Task<T> GetAllAsync(T entity);
        Task<T> AddAsync(T entity);
        Task<T> RemoveAsync(T entity);
        Task<T> UpdateAsync(T entity);
    }
}