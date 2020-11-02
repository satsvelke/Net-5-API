using Model;
using Persistence.Repository;
using System.Threading.Tasks;

namespace Persistence.UnitOfWork
{
    public interface IWork
    {
        GenericRepository<User> UserRepository { get; }

        void Dispose();
        Task<int> Save();
    }
}