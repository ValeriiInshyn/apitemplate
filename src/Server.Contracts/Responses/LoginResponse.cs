using Swashbuckle.AspNetCore.Annotations;

namespace Server.Contracts.Responses;


[SwaggerSchema("Successful login response")]
public sealed record LoginResponse
{
    [SwaggerSchema("String representation of jwt token")]
    public string AccessToken { get; init; } = null!;

    [SwaggerSchema("The date and time when token will be expired")]
    public DateTimeOffset AccessTokenExpiration { get; init; }

    [SwaggerSchema("Authorized username")] 
    public string Username { get; init; } = null!;
    
}