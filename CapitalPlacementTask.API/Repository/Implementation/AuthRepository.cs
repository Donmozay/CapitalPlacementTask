using CapitalPlacementTask.API.Data;
using CapitalPlacementTask.API.Models;
using CapitalPlacementTask.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CapitalPlacementTask.API.Repository.Implementation
{
    public class AuthRepository: IAuthRepository
    {
        private readonly DataContext _context;
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ApplicationUser?>GetUserAsync(string emailAddress)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(x => x.Email == emailAddress);
            }
            catch (Exception ex)
            {
                _logger.Error($"An Error occured. Message: {ex.Message} ||\n InnerException: {ex.InnerException} || StackTrace: {ex.StackTrace}");
                return null;
            }
        }

        public async Task<List<ApplicationUser>?> GetUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"An Error occured. Message: {ex.Message} ||\n InnerException: {ex.InnerException} || StackTrace: {ex.StackTrace}");
                return null;
            }
        }
    }
}
