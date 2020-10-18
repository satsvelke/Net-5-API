using System.Threading.Tasks;
using Model;

namespace Persistence.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByEmail(User user);
    }
}