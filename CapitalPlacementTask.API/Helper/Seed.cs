using CapitalPlacementTask.API.Data;
using CapitalPlacementTask.API.Models;
using Microsoft.AspNetCore.Identity;

namespace CapitalPlacementTask.API.Helper
{
    public class Seed
    {
        public static async Task SeedUser(IApplicationBuilder applicationBuilder, UserManager<ApplicationUser> _userManager)
        {
            try
            {
                var employerEmail = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("UserAuthDetails")["CandidtePassword"];
                using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<DataContext>();
                    context.Database.EnsureCreated();
                    var hasher = new PasswordHasher<ApplicationUser>();
                    var getApplicant = context.Users.FirstOrDefault(x => x.Role == Role.Candidate);
                    if (getApplicant == null)
                    {
                        var newquest = new ApplicationUser
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("UserAuthDetails")["CandidateEmail"]?.Split("@")[0],
                            NormalizedUserName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("UserAuthDetails")["CandidateEmail"]?.Split("@")[0],
                            PasswordHash = hasher.HashPassword(null, password: new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("UserAuthDetails")["CandidtePassword"]),
                            Email = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("UserAuthDetails")["CandidateEmail"],
                            NormalizedEmail = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("UserAuthDetails")["CandidateEmail"],
                            Role = Role.Candidate
                        };
                        var result = await _userManager.CreateAsync(newquest);
                    }
                    var getEmployer = context.Users.FirstOrDefault(x => x.Role == Role.Employer);
                    if (getEmployer == null)
                    {
                        var newquest = new ApplicationUser
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("UserAuthDetails")["EmployerEmail"]?.Split("@")[0],
                            NormalizedUserName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("UserAuthDetails")["EmployerEmail"]?.Split("@")[0],
                            PasswordHash = hasher.HashPassword(null, password: new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("UserAuthDetails")["EmployerPassword"]),
                            Email = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("UserAuthDetails")["EmployerEmail"],
                            NormalizedEmail = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("UserAuthDetails")["EmployerEmail"],
                            Role = Role.Employer
                        };
                        var result = await _userManager.CreateAsync(newquest);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
