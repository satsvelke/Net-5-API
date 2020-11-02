using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModel;

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