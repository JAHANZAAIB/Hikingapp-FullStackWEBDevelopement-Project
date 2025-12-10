using HikingApp.Models.Domain;
using HikingApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HikingApp.Repositories
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly MyAppContext _context;
        public SQLWalkRepository(MyAppContext context)
        {
            _context = context;
        }
        public async Task<Walk> Create(Walk walk)
        {
            await _context.Walks.AddAsync(walk);
            await _context.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> Delete(Guid id)
        {
            var walks = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walks == null)
            {
                return walks;
            }
            _context.Walks.Remove(walks);
            await _context.SaveChangesAsync();

            return walks;
        }

        public async Task<List<Walk>> GetAllAsync(Guid? regionId = null, string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAssending = true)
        {
            var walks = _context.Walks
                .Include(w => w.difficulty)
                .Include(w => w.Region)
                .Include(w => w.Images)
                .Include(w => w.Ratings)
                .AsQueryable();

            if (regionId.HasValue && regionId != Guid.Empty)
            {
                walks = walks.Where(w => w.RegionId == regionId.Value);
            }
            
            // Filtering
            
            if (string.IsNullOrWhiteSpace(filterOn) == false &&
                string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
                else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Description.Contains(filterQuery));
                }
                else if(filterOn.Equals("LengthInKM", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.LengthInKM.ToString().Contains(filterQuery));
                }
            }


            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAssending? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                if (sortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAssending ? walks.OrderBy(x => x.LengthInKM) : walks.OrderByDescending(x => x.LengthInKM);
                }
            }

            return await walks.ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _context.Walks
                .Include(w => w.Region)
                .Include(w => w.difficulty)
                .Include(w => w.Images)
                .Include(w => w.Ratings)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> Update(Guid id, Walk walk)
        {
            var walks = await _context.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walks == null)
            {
                return walks;
            }

            walks.LengthInKM = walk.LengthInKM;
            walks.Name = walk.Name;
            walks.Description = walk.Description;
            walks.WalkImageUrl= walk.WalkImageUrl;
            walks.RegionId = walk.RegionId;
            walks.DifficultyId = walk.DifficultyId;
            walks.RouteGeometry = walk.RouteGeometry;
            walks.ElevationGainMeters = walk.ElevationGainMeters;
            walks.EstimatedDurationMinutes = walk.EstimatedDurationMinutes;
            walks.IsAccessible = walk.IsAccessible;
            walks.Features = walk.Features;

            await _context.SaveChangesAsync();

            return walks;
        }
    }
}
