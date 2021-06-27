using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.ViewModel;
using Workflow.Interface;

namespace Service.Controllers
{
    public partial class UserController : CoreController
    {
        private readonly IUserLogic userLogic;
        public UserController(IUserLogic iUserLogic) => userLogic = iUserLogic;

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user) => Ok(await userLogic.CreateUserAsync(user).ConfigureAwait(true));
    }
}