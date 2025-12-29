using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HikingApp.CustomActionFilters;
using HikingApp.Data;
using HikingApp.Models.Domain;
using HikingApp.Models.DTO;
using HikingApp.Repositories;

namespace HikingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
         private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionsController> logger;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper,
            ILogger<RegionsController> logger)
        {
            this.regionRepository= regionRepository;
            this.mapper= mapper;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var regions = await regionRepository.GetAllAsync();

            return Ok(mapper.Map<List<RegionDTO>>(regions));

        }

        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regions = await regionRepository.GetbyId(id);
            if (regions == null)
            {
                return NotFound();
            }


            return Ok(mapper.Map<RegionDTO>(regions));
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRequestDto addRequestDto)
        {
            var regions = mapper.Map<Region>(addRequestDto);

            regions = await regionRepository.Create(regions);

            var regionsDto =mapper.Map<RegionDTO>(regions);

            // You can return the created region or its location
            return CreatedAtAction(nameof(GetById), new { id = regionsDto.Id }, regionsDto);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRequestDto updateRequestDto)
        {
            var regions = mapper.Map<Region>(updateRequestDto);

            regions = await regionRepository.Update(id, regions);
            if (regions == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<RegionDTO>(regions));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regions= await regionRepository.Delete(id);
            if (regions == null)
            {
                return NotFound();
            }

            return Ok("Region Deleted");
        }
    }
}
