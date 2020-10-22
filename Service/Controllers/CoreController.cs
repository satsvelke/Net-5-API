using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Controllers.Filters;

namespace Service.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    [TypeFilter(typeof(ExceptionFilter))]
    public partial class CoreController : ControllerBase
    {

    }
}