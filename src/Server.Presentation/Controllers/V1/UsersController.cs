using Microsoft.AspNetCore.Mvc;

namespace Server.Presentation.Controllers.V1;

[ApiVersion("1.0", Deprecated = false)]
public class UsersController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> OkMessage()
    {
        return Ok("Good");
    }
}