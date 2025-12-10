namespace HikingApp.Repositories;
using HikingApp.Models;
using HikingApp.Models.Domain;

public interface IRegionRepository
{
    Task<List<Region>> GetAllAsync();
    Task<Region?> GetbyId(Guid id);

    Task<Region> Create(Region region);

    Task<Region?> Update(Guid id, Region region);

    Task<Region?> Delete(Guid id);
}
