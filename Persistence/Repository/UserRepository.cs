using Model;
using Persistence.Interface;
using Persistence.UnitOfWork;
using System.Linq;
using System.Threading.Tasks;
namespace Persistence.Repository
{
    public partial class UserRepository : IUserRepository
    {
        private readonly IWork work;
        public UserRepository(IWork _work) => work = _work;

        public async Task<User> AddAsync(User entity)
        {
            work.UserRepository.Insert(entity);
            await work.Save();
            return entity;
        }
        public async Task<User> GetUserByEmail(User user)
        {
            var filteredList = await work.UserRepository.GetAsync(filter: u => u.Email == user.Email && u.IsActive == true);
            return filteredList.FirstOrDefault();
        }
    }
}