using System;
using System.Threading.Tasks;
using ViewModel;

namespace BusinessLayer.Interface
{
    public interface IUserLogic
    {
        Task<UserViewModel> CreateUserAsync(UserViewModel user);
        Task<Tuple<UserViewModel, GenericMessage>> CreateTokenAsync(UserViewModel user);
    }
}