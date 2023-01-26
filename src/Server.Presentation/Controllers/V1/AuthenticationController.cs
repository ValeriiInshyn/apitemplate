using Microsoft.AspNetCore.Mvc;

namespace Server.Presentation.Controllers.V1;

[ApiVersion("1.0", Deprecated = false)]
public class AuthenticationController : BaseApiController
{
    [HttpGet]
    public Task Login()
    {
        return Task.CompletedTask;
    }
}