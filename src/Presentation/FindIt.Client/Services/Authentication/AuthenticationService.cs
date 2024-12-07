using FindIt.Shared.Authentications;
using System.Net.Http.Json;

namespace FindIt.Client.Services.Authentication;
public class AuthenticationService(HttpClient http) : IAuthenticationService
{
	public async Task<AppUserResponse?> RegisterAsync(RegisterRequest registerRequest)
	{
        try
        {
            var response = await http.PostAsJsonAsync("api/v1/auth/register", registerRequest);

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<AppUserResponse>();

            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

	public async Task<AppUserResponse?> LoginAsync(LoginRequest loginRequest)
	{
		try
		{
			var response = await http.PostAsJsonAsync("api/v1/auth/login", loginRequest);

			if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<AppUserResponse>();
				
            return null;
		}
		catch (Exception)
		{
			return null;
		}
	}
}