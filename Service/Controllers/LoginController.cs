using System.Threading.Tasks;
using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ViewModel;

namespace Service.Controllers
{

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly IUserLogic _userLogic;
        public LoginController(IUserLogic IUserLogic)
        {
            _userLogic = IUserLogic;
        }

        [HttpGet]
        public async Task<IActionResult> GetToken(UserViewModel user)
        {
            return Ok(await _userLogic.CreateTokenAsync(user));
        }
    }
}