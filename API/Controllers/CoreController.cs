using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]

    /// <summary>
    /// base api controller for all controllers
    /// </summary>
    public partial class CoreController : ControllerBase
    {

    }
}