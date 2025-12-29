using HikingApp.Data;
using HikingApp.Models.Domain;
using HikingApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HikingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkRatingsController : ControllerBase
    {
        private readonly MyAppContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public WalkRatingsController(MyAppContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddRating([FromBody] ReqWalkRatingDto ratingDto)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            // Check if walk exists
            var walk = await _context.Walks.FindAsync(ratingDto.WalkId);
            if (walk == null) return NotFound("Walk not found");

            var rating = new WalkRating
            {
                Id = Guid.NewGuid(),
                WalkId = ratingDto.WalkId,
                UserId = user.Id,
                Rating = ratingDto.Rating,
                Comment = ratingDto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _context.WalkRatings.AddAsync(rating);
            await _context.SaveChangesAsync();

            return Ok(rating);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteRating(Guid id)
        {
            var rating = await _context.WalkRatings.FindAsync(id);
            if (rating == null) return NotFound();

            _context.WalkRatings.Remove(rating);
            await _context.SaveChangesAsync();

            return Ok("Rating deleted");
        }
    }
}
