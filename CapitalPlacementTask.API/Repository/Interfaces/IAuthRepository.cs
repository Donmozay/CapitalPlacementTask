using CapitalPlacementTask.API.Models;

namespace CapitalPlacementTask.API.Repository.Interfaces
{
    public interface IAuthRepository
    {
        Task<ApplicationUser?> GetUserAsync(string emailAddress);
        Task<List<ApplicationUser>?> GetUsersAsync();
    }
}
