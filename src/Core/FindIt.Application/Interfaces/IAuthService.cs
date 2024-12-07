using FindIt.Application.ErrorHandling;
using FindIt.Shared.Authentications;


namespace FindIt.Application.Interfaces;

public interface IAuthService
{
    Task<Result<AppUserResponse>> RegisterUserAsync(RegisterRequest register);
    Task<Result<AppUserResponse>> LoginUserAsync(LoginRequest register);
    Task LogoutUserAsync();
    Task<Result> AssignUserToRoleAsync(string email, string role);
    Task<AppUserResponse> GetUserAsync();
    Task<Result> DeleteUserAsync();
}
