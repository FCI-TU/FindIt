using FindIt.Shared.Authentications;

namespace FindIt.Client.Services.Authentication;
public interface IAuthenticationService
{
	Task<AppUserResponse?> RegisterAsync(RegisterRequest registerRequest);
	Task<AppUserResponse?> LoginAsync(LoginRequest loginRequest);
}