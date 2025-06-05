using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.ViewModels;

public class UserLoginViewModel
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name must contain only alphabets")]
    [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
    public string Name { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required.")]
    [MaxLength(50, ErrorMessage = "Email cannot exceed 50 characters.")]
    public string Email { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [MaxLength(20, ErrorMessage = "Password cannot exceed 20 characters.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$", ErrorMessage = "Password must contain at least one uppercase letter, one number, and one special character.")]
    public string Password { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Confirm Password is required")]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }
    public bool Remember_me { get; set; }

}