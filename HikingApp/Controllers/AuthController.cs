using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HikingApp.Models.DTO;
using HikingApp.Repositories;

namespace HikingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager,
            ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
                    // 1. Check if roles exist
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    foreach (var role in registerRequestDto.Roles)
                    {
                        var roleExists = await roleManager.RoleExistsAsync(role);
                        if (!roleExists)
                        {
                            return BadRequest($"Validation Error: Role '{role}' does not exist in the system. Please contact admin.");
                        }
                    }
                }

                // 2. Create User
                var identityUser = new IdentityUser
                {
                    UserName = registerRequestDto.username,
                    Email = registerRequestDto.username
                };

                var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.password);

                if (!identityResult.Succeeded)
                {
                    // Return specific Identity error messages (Password complexity, Duplicate email, etc.)
                    return BadRequest(identityResult.Errors);
                }

                // 3. Assign Roles
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                     var roleResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                     if (!roleResult.Succeeded)
                     {
                         // Rollback user creation if role assignment fails? 
                         // For now, just return the error. ideally we'd delete the user.
                         await userManager.DeleteAsync(identityUser);
                         return BadRequest(roleResult.Errors);
                     }
                }

                return Ok("User Registered! Please Login");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.username);

            if (user == null)
            {
                return BadRequest("Email Not Found");
            }

            var passwordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.password);

            if(passwordResult)
            {
                var roles = await userManager.GetRolesAsync(user);

                if (roles != null)
                {
                    // Create Token
                    var JwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());
                    return Ok(JwtToken);
                }

            }
            return BadRequest("Incorrect Password");

        }
    }
}
