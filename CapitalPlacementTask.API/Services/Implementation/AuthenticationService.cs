using CapitalPlacementTask.API.Models;
using CapitalPlacementTask.API.Repository.Interfaces;
using CapitalPlacementTask.API.Responses;
using CapitalPlacementTask.API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CapitalPlacementTask.API.Services.Implementation
{
    public class AuthenticationService : IAuthService
    {
        // Specify how long until the token expires
        private const int ExpirationMinutes = 30;
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IAuthRepository _authRepo;
        public AuthenticationService(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        public async Task<GenericResponse<ApplicationUser?>>GetUserAsync(string emailAddress)
        {
            var getUser = await _authRepo.GetUserAsync(emailAddress);
            if (getUser!= null)
            {
                return new GenericResponse<ApplicationUser?> { Data = getUser, IsSuccessful = true, ResponseCode = "00", ResponseDescription = "Success" };
            }
            return new GenericResponse<ApplicationUser?> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = "Failed" };
        }

        public async Task<GenericResponse<List<ApplicationUser?>>> GetUsersAsync()
        {
            var getUser = await _authRepo.GetUsersAsync();
            if (getUser.Any())
            {
                return new GenericResponse<List<ApplicationUser?>> { Data = getUser, IsSuccessful = true, ResponseCode = "00", ResponseDescription = "Success" };
            }
            return new GenericResponse<List<ApplicationUser?>> { IsSuccessful = false, ResponseCode = "99", ResponseDescription = "No record found" };
        }

        public string CreateToken(ApplicationUser user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
            var token = CreateJwtToken(
                CreateClaims(user),
                CreateSigningCredentials(),
                expiration
            );
            var tokenHandler = new JwtSecurityTokenHandler();

            _logger.Info("JWT Token created");

            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
            DateTime expiration) =>
            new(
                new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt")["ValidIssuer"],
                new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt")["ValidAudience"],
                claims,
                expires: expiration,
                signingCredentials: credentials
            );

        private List<Claim> CreateClaims(ApplicationUser user)
        {
            var jwtSub = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt")["JwtRegisteredClaimNamesSub"];

            try
            {
                var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwtSub),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

                return claims;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private SigningCredentials CreateSigningCredentials()
        {
            var symmetricSecurityKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt")["Secret"];

            return new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(symmetricSecurityKey)
                ),
                SecurityAlgorithms.HmacSha256
            );
        }
    }
}
