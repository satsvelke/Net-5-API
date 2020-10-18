using System.Threading.Tasks;
using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ViewModel;

namespace Service.Controllers
{
    public partial class UserController : CoreController
    {
        private readonly IUserLogic _userLogic;
        public UserController(IUserLogic IUserLogic)
        {
            _userLogic = IUserLogic;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user) => Ok(await _userLogic.CreateUserAsync(user));
    }
}