using CapitalPlacementTask.API.Models;
using CapitalPlacementTask.API.Responses;

namespace CapitalPlacementTask.API.Services.Interfaces
{
    public interface IAuthService
    {
        string CreateToken(ApplicationUser user);
        Task<GenericResponse<ApplicationUser?>> GetUserAsync(string emailAddress);
        Task<GenericResponse<List<ApplicationUser?>>> GetUsersAsync();
    }
}
