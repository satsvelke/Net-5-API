using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Service.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public partial class CoreController : ControllerBase
    {

    }
}