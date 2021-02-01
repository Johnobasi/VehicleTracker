using Microsoft.AspNetCore.Mvc;

namespace VehicleTracker.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
    }
}
