using Microsoft.AspNetCore.Mvc;

namespace Server.Presentation.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Produces("application/json")]
public abstract class BaseApiController : ControllerBase
{
}