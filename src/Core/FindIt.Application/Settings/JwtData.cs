namespace FindIt.Application.Settings;
public class JwtData
{
    public string SecretKey { get; set; } = null!;
    public string ValidAudience { get; set; } = null!;
    public string ValidIssuer { get; set; } = null!;
    public double DurationInDays { get; set; } 
}