using FindIt.Application.ErrorHandling;
using FindIt.Application.Interfaces;
using FindIt.Application.Settings;
using FindIt.Domain.IdentityEntities;
using FindIt.Shared.Authentications;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FindIt.Application.Services;

public class AuthService
    (UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IOptions<JwtData> jwtDataOptions,
        RoleManager<IdentityRole> roleManager) : IAuthService
{
    private readonly JwtData _jwtData = jwtDataOptions.Value;

    public async Task<Result<AppUserResponse>> LoginUserAsync(LoginRequest login)
    {
        var user = await userManager.FindByEmailAsync(login.Email);
        if (user is null)
        {
            return Result.Failure<AppUserResponse>(new Status(StatusCode.NotFound, "No user registered with this email."));
        }

        var signInResult = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        if (!signInResult.Succeeded)
        {
            return Result.Failure<AppUserResponse>(new Status(StatusCode.BadRequest, "Incorrect email or password."));
        }

        return await CreateUserResponse(user);
    }

    public async Task<Result<AppUserResponse>> RegisterUserAsync(RegisterRequest register)
    {
        if (await userManager.FindByEmailAsync(register.Email) != null)
        {
            return Result.Failure<AppUserResponse>(new Status(StatusCode.Conflict, "A user with this email already exists."));
        }

        var user = new AppUser
        {
            Email = register.Email,
            UserName = register.Email.Split('@')[0],
            FirstName = register.FirstName,
            LastName = register.LastName,
            PhoneNumber = register.PhoneNumber
        };

        var result = await userManager.CreateAsync(user, register.Password);
        if(!result.Succeeded)
            return Result.Failure<AppUserResponse>(new Status(StatusCode.BadRequest, "User registration failed."));
        
        var assignUserToRole = await AssignUserToRoleAsync(user.Email, "user");
        if (!assignUserToRole.IsSuccess)
        {
            return Result.Failure<AppUserResponse>(new Status(StatusCode.BadRequest, assignUserToRole.Status!.Message));
        }

        return await CreateUserResponse(user);
    }

    public async Task LogoutUserAsync()
    {
        await signInManager.SignOutAsync();
    }

    public async Task<Result> AssignUserToRoleAsync(string email, string role)
    {
        // Find the user by their email
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Result.Failure(new Status(StatusCode.NotFound, "User not found."));
        }

        // Check if the role exists 
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }

        // Assign the user to the role
        var result = await userManager.AddToRoleAsync(user, role);
        if (result.Succeeded) return Result.Success("User successfully assigned to role.");

        // Collect error messages if the operation fails
        var errors = result.Errors.Select(e => e.Description).ToList();
        return Result.Failure(new Status(StatusCode.BadRequest, string.Join("; ", errors)));

    }

    public Task<AppUserResponse> GetUserAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteUserAsync()
    {
        throw new NotImplementedException();
    }


    private async Task<string> GenerateAccessTokenAsync(AppUser user)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.GivenName, user.UserName!),
            new(ClaimTypes.Email, user.Email!)
        };

        var userRoles = await userManager.GetRolesAsync(user);
        authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtData.SecretKey));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddDays(_jwtData.DurationInDays),
            Claims = authClaims.ToDictionary(c => c.Type, object (c) => c.Value),
            Audience = _jwtData.ValidAudience,
            Issuer = _jwtData.ValidIssuer,
            SigningCredentials = new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    private async Task<Result<AppUserResponse>> CreateUserResponse(AppUser user)
    {
        var token = await GenerateAccessTokenAsync(user);
        var userRoles = await userManager.GetRolesAsync(user);

        var userResponse = new AppUserResponse
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber!,
            Token = token,
            IsVerified = user.EmailConfirmed,
            Roles = userRoles
        };

        return Result.Success<AppUserResponse>(userResponse);
    }
}

