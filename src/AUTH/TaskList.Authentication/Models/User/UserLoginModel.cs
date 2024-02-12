using System.ComponentModel.DataAnnotations;

namespace TaskList.Authentication.Models.User
{
    public sealed class UserLoginModel
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
