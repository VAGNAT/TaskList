using System.ComponentModel.DataAnnotations;

namespace TaskList.Authentication.Models.User
{
    public sealed class UserRegisterModel
    {
        [Required]
        [StringLength(30)]
        [RegularExpression(@"^[a-zA-Z]+$",ErrorMessage = "You can only use Latin letters")]
        public required string Name { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password mismatch.")]
        public string? ConfirmPassword { get; set; }
    }
}
