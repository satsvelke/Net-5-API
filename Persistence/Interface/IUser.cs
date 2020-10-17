namespace Persistence.Interface
{
    public interface IUser<T> : IGenericRepository<T> where T : class
    {

    }
}