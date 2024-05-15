using CapitalPlacementTask.API.Models;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementTask.API.Dtos
{
    public class RegistrationDto
    {
        [Required(ErrorMessage = "Email is required"), RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }

        [Required(ErrorMessage ="Usernme is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        public Role Role { get; set; }
    }
}
