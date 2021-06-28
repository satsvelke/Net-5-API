using System.Threading.Tasks;
using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Workflow.Interface
{
    public interface IUserLogic
    {
        Task<UserViewModel> CreateUserAsync(UserViewModel user);
        Task<IActionResult> CreateTokenAsync(UserViewModel user);
    }
}