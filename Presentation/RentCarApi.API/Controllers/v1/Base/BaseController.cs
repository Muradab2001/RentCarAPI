
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace RentCarApi.API.Controllers.v1.Base
{
    [ApiVersion("1.0")]
    [EnableRateLimiting("Basic")]
    [ApiController]
    [Route("Api/V1/[Controller]/[Action]")]
    public class BaseController : ControllerBase
    {
    }
}