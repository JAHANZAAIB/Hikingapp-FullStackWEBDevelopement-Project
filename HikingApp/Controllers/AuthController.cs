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
            // ✅ Step 1: Check if all roles exist
            foreach (var role in registerRequestDto.Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    return BadRequest($"Role '{role}' does not exist");
                }
            }

            // ✅ Step 2: Create user
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.username,
                Email = registerRequestDto.username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.password);

            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors); // shows password errors
            }

            // ✅ Step 3: Assign roles
            var roleResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

            if (!roleResult.Succeeded)
            {
                return BadRequest(roleResult.Errors);
            }

            return Ok("User Registered! Please Login");
        }

        [HttpPost]
        [Route("Loin")]
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
