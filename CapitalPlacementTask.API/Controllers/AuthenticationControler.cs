using CapitalPlacementTask.API.Data;
using CapitalPlacementTask.API.Dtos;
using CapitalPlacementTask.API.Models;
using CapitalPlacementTask.API.Responses;
using CapitalPlacementTask.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CapitalPlacementTask.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthenticationControler : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public AuthenticationControler(UserManager<ApplicationUser> userManager, DataContext context, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _userManager.CreateAsync(
                    new ApplicationUser { UserName = request.Username, Email = request.Email, Role = request.Role == Role.Candidate ? Role.Candidate : Role.Employer },
                    request.Password!
                );

                if (result.Succeeded)
                {
                    return Ok(new GenericResponse<dynamic> { IsSuccessful = true, ResponseCode = "00", ResponseDescription = "Success" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.Error($"An Error occured. Message: {ex.Message} ||\n InnerException: {ex.InnerException} || StackTrace: {ex.StackTrace}");
                return StatusCode(500, new {error = "internal error occured"});
            }
           
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var managedUser = await _userManager.FindByEmailAsync(request.Email);
                if (managedUser == null)
                {
                    return BadRequest("Bad credentials");
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
                if (!isPasswordValid)
                {
                    return BadRequest("Bad credentials");
                }

                var userInDb = await _authService.GetUserAsync(request.Email);

                if (!userInDb.IsSuccessful)
                {
                    return Unauthorized();
                }

                var accessToken = _authService.CreateToken(userInDb.Data);
                return Ok(new GenericResponse<AuthResponse>
                {
                    IsSuccessful = true,
                    Data = new AuthResponse { Email = request.Email, Token = accessToken, Username = userInDb.Data?.UserName },
                    ResponseCode = "00",
                    ResponseDescription = "Success"
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"An Error occured. Message: {ex.Message} ||\n InnerException: {ex.InnerException} || StackTrace: {ex.StackTrace}");
                return StatusCode(500, new { error = "internal error occured" });
            }
            
        }

        [HttpGet]
        [Authorize(Roles = "Employer")]
        [Route("get-user{email}")]
        public async Task<ActionResult> Getuser(string email)
        {
            var userInDb = await _authService.GetUserAsync(email);
            return Ok(userInDb);
        }

        [HttpGet]
        [Authorize(Roles = "Employer")]
        [Route("get-users")]
        public async Task<ActionResult> Getusers()
        {
            var userInDb = await _authService.GetUsersAsync();
            return Ok(userInDb);
        }
    }
}
