using Asp.Versioning;
using FindIt.Application.Interfaces;
using FindIt.Server.Extensions;
using FindIt.Shared.Authentications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindIt.Server.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<AppUserResponse>> Register(RegisterRequest model)
        {
            var result = await authService.RegisterUserAsync(model);

            return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserResponse>> Login(LoginRequest model)
        {
            var result = await authService.LoginUserAsync(model);

			return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
		}

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await authService.LogoutUserAsync();
            return Ok(new { message = "User logged out successfully" });
        }

        [Authorize(Roles = "admin")]
        [HttpPost("assign-role")]
        public async Task<ActionResult> AssignRole(string email, string roleName)
        {
            var result = await authService.AssignUserToRoleAsync(email, roleName);

            return result.IsSuccess ? result.ToSuccess() : result.ToProblem();
        }
    }
}
