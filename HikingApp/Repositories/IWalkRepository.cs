using HikingApp.Models.Domain;

namespace HikingApp.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> Create(Walk walk);
        Task<List<Walk>> GetAllAsync(Guid? regionId = null, string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAssending = true);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk?> Update(Guid id, Walk walk);

        Task<Walk?> Delete(Guid id);
    }
}
