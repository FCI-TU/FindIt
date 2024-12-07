using System.ComponentModel.DataAnnotations;

namespace FindIt.Shared.Authentications;
public class RegisterRequest
{
    [Required(ErrorMessage = "First Name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 100 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last Name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 100 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    [StringLength(100, ErrorMessage = "Email must be at most 100 characters")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone Number is required")]
    [Phone(ErrorMessage = "Invalid Phone Number")]
    [StringLength(11, ErrorMessage = "Phone Number must be at most 11 characters")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    [MaxLength(100, ErrorMessage = "Password must be at most 100 characters")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,100}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Re-Password is required")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and Re-Password do not match")]
    public string RePassword { get; set; } = string.Empty;
}