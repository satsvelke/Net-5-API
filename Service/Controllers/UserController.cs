using System.Threading.Tasks;
using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ViewModel;

namespace Service.Controllers
{
    public partial class UserController : CoreController
    {
        private readonly IUserLogic userLogic;
        public UserController(IUserLogic iUserLogic) => this.userLogic = iUserLogic;

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user) => Ok(await this.userLogic.CreateUserAsync(user));
    }
}