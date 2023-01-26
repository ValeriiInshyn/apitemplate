using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Server.Contracts.Responses;
using Server.Domain.Scaffolded;
using Server.Presentation.Options;

namespace Server.Presentation.Controllers.V1;

[ApiVersion("1.0", Deprecated = false)]
public class AuthenticationController : BaseApiController
{
    private readonly AuthOptions _authOptions;

    public AuthenticationController(IOptions<AuthOptions>  authOptions)
    {
        _authOptions = authOptions.Value;
    }
    [HttpGet("GetTestAuthToken")]
    public async Task<IActionResult> GetTestAuthToken()
    {
        User testUser = new User()
        {
            UserName = "Test",
            Password = "123",
            Email = "Test@gmail.com",
            GroupId = 1,
            IsDeleted = false
        };
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, testUser.UserName),
        };
        
        var identity = new ClaimsIdentity(claims,
            JwtBearerDefaults.AuthenticationScheme,
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        
        var now = DateTime.UtcNow;
        var expirationDate = now.Add(TimeSpan.FromMinutes(_authOptions.AccessTokenLifetime));
        
        var jwt = new JwtSecurityToken(
            _authOptions.Issuer,
            _authOptions.Audience,
            notBefore: now,
            claims: claims,
            expires: expirationDate,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authOptions.Key)),
                SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
        return Ok(new LoginResponse
        {
            AccessToken = encodedJwt,
            AccessTokenExpiration = expirationDate,
            Username = identity.Name!
        });
    }
    [HttpGet("testgeneratedtoken")]
    [Authorize]
    public async Task<IActionResult> TestGeneratedToken()
    {
        return Ok($"Token generated succesfullty and works well!/n You have been successfully authenticated as {User.Identity.Name}");
    }
}