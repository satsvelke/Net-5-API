using System.Threading.Tasks;
using ViewModel;

namespace BusinessLayer.Interface
{
    public interface IUserLogic
    {
        Task<UserViewModel> CreateUserAsync(UserViewModel user);
    }
}