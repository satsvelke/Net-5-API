using System.Threading.Tasks;
using Model;

namespace Persistence.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(User user);
        Task<User> AddAsync(User entity);
    }
}