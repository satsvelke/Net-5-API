using System;
using System.Threading.Tasks;
using Domain.ViewModel;

namespace Workflow.Interface
{
    public interface IUserLogic
    {
        Task<UserViewModel> CreateUserAsync(UserViewModel user);
        Task<Tuple<UserViewModel, GenericMessage>> CreateTokenAsync(UserViewModel user);
    }
}