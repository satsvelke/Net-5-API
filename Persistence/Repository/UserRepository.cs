using Microsoft.EntityFrameworkCore;
using Model;
using Persistence.DatabaseContext;
using Persistence.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SpecificContext context;
        public UserRepository(SpecificContext _context) => context = _context;

        public async Task<User> AddAsync(User entity)
        {
            using (context)
            {
                context.Add(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public Task<User> GetAllAsync(User entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetAsync(User entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUserByEmail(User user)
        {
            using (context)
            {
                IQueryable<User> q = context.Users.Where(u => u.Email == user.Email && u.IsActive == true);
                return await q.FirstOrDefaultAsync();
            }
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