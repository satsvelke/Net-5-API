using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Domain.ViewModel;
using Workflow.Interface;

namespace Service.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public partial class LoginController : ControllerBase
    {
        private readonly IUserLogic _userLogic;
        public LoginController(IUserLogic IUserLogic) => _userLogic = IUserLogic;

        /// <summary>
        ///  Gets the authentication token
        /// </summary>
        /// <param name="user"> Email and Password</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetToken(UserViewModel user) 
            => await _userLogic.CreateTokenAsync(user).ConfigureAwait(true);
    }
}