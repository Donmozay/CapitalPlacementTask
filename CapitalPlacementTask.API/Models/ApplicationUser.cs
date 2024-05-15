using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CapitalPlacementTask.API.Models
{
    public enum Role
    {
        Employer,
        Candidate
    }
    public class ApplicationUser : IdentityUser
    {
        public Role Role { get; set; }
    }
}
