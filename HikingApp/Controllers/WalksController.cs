using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using HikingApp.CustomActionFilters;
using HikingApp.Models.Domain;
using HikingApp.Models.DTO;
using HikingApp.Repositories;
using System.Linq;

namespace HikingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;   
        }

        [HttpPost]
        [ValidateModel]

        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            var walks = mapper.Map<Walk>(addWalkRequestDto);
            walks = await walkRepository.Create(walks);
            return Ok(mapper.Map<WalkDto>(walks));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid? regionId, [FromQuery]string? filterOn,[FromQuery]string? filterQuery,
        [FromQuery] string? sortBy, [FromQuery] bool? isAssending   )
        {
            var walks = await walkRepository.GetAllAsync(regionId, filterOn,filterQuery,sortBy,isAssending ?? true);

            return Ok(mapper.Map<List<WalkDto>>(walks));
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var walks = await walkRepository.GetByIdAsync(id);
            if (walks == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walks));
        }

        [HttpGet("map/{id:guid}")]
        public async Task<IActionResult> GetWalkForMap(Guid id)
        {
            var walk = await walkRepository.GetByIdAsync(id);

            if (walk == null)
                return NotFound();

            var dto = new WalkMapDetailDto
            {
                Id = walk.Id,
                Name = walk.Name,
                Description = walk.Description,
                LengthInKM = walk.LengthInKM,
                DifficultyId = walk.DifficultyId,
                DifficultyName = walk.difficulty.Name,
                RegionId = walk.RegionId,
                RegionName = walk.Region.Name,
                RouteGeometry = walk.RouteGeometry,
                ElevationGainMeters = walk.ElevationGainMeters,
                EstimatedDurationMinutes = walk.EstimatedDurationMinutes,
                IsAccessible = walk.IsAccessible,
                Features = walk.Features,
                ImageUrls = walk.Images.Select(i => i.FilePath).ToList(),
                AverageRating = walk.Ratings.Any() ? Math.Round(walk.Ratings.Average(r => r.Rating), 2) : 0,
                RatingsCount = walk.Ratings.Count
            };

            return Ok(dto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalksDto updateWalksDto)
        {
            var walks = mapper.Map<Walk>(updateWalksDto);

            walks = await walkRepository.Update(id, walks);
            if (walks == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<WalkDto>(walks));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var walks = await walkRepository.Delete(id);
            if (walks == null)
            {
                return NotFound();
            }

            return Ok("Walk Deleted");
        }
    }
}
