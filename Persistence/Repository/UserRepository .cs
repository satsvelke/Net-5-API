using Model;
using Persistence.DatabaseContext;
using Persistence.Interface;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UserRepository : IUser<User>
    {
        private readonly SpecificContext context;
        public UserRepository(SpecificContext _context) => context = _context;

        public Task<User> AddAsync(User entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetAllAsync(User entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetAsync(User entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> RemoveAsync(User entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> UpdateAsync(User entity)
        {
            throw new System.NotImplementedException();
        }
    }
}