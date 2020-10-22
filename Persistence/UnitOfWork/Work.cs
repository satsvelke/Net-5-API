using System;
using System.Threading.Tasks;
using Model;
using Persistence.DatabaseContext;
using Persistence.Repository;

namespace Persistence.UnitOfWork
{
    public partial class Work : IDisposable, IWork
    {
        private readonly SpecificContext context;

        private GenericRepository<User> userRepository;

        public Work(SpecificContext _context)
        {
            context = _context;
        }
        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }


        public async Task<int> Save()
        {
            return await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}