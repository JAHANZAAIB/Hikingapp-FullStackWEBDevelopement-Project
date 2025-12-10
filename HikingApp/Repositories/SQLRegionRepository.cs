using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using HikingApp.Data;
using HikingApp.Models.Domain;
using HikingApp.Models.DTO;

namespace HikingApp.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly MyAppContext _context;
        public SQLRegionRepository(MyAppContext context) 
        {
            _context = context;
        }

        public async Task<Region> Create(Region region)
        {
            var regions = new Region
            {
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };

            await _context.Regions.AddAsync(regions);
            await _context.SaveChangesAsync();
            return regions;
        }

        public async Task<Region?> Delete(Guid id)
        {
            var regions = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regions == null)
            {
                return regions;
            }
            _context.Regions.Remove(regions);
            await _context.SaveChangesAsync();

            return regions;
        }

        public async  Task<List<Region>> GetAllAsync()
        {
             return await _context.Regions.ToListAsync();
        }

        public async Task<Region?> GetbyId(Guid id)
        {
           return await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
           
        }

        public async Task<Region?> Update(Guid id, Region region)
        {
            var regions = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (regions == null)
            {
                return regions;
            }

            regions.Code = region.Code;
            regions.Name = region.Name;
            regions.RegionImageUrl = region.RegionImageUrl;

            await _context.SaveChangesAsync();

            return regions;
        }
    }
}
