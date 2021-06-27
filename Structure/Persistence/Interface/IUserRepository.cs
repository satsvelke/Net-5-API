using Domain.Model;
using System.Threading.Tasks;

namespace Persistence.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(User user);
        Task<User> AddAsync(User entity);
    }
}