using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModel;
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
        [HttpGet]
        public async Task<IActionResult> GetToken(UserViewModel user)
        {
            var response = await _userLogic.CreateTokenAsync(user).ConfigureAwait(true);
            //if (response.Item1 == null) return BadRequest(response.Item2); else return Ok(response.Item1); // 8.0
            return response.Item1 == null ?  BadRequest(response) :  Ok(response.Item1); //9.0
        }
    }
}