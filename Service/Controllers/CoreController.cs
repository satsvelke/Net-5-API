using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Filters;

namespace Service.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    //[TypeFilter(typeof(ExceptionFilter))] // deprecated
    public partial class CoreController : ControllerBase
    {

    }
}