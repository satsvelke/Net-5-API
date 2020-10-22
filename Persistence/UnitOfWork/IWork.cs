using System.Threading.Tasks;
using Model;
using Persistence.Repository;

namespace Persistence.UnitOfWork
{
    public interface IWork
    {
        GenericRepository<User> UserRepository { get; }

        void Dispose();
        Task<int> Save();
    }
}