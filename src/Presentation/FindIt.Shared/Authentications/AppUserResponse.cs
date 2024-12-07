namespace FindIt.Shared.Authentications;
public class AppUserResponse
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public required IList<string> Roles { get; set; }
    public bool IsVerified { get; set; }
}