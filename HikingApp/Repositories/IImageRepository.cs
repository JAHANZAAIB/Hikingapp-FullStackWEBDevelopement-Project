using HikingApp.Models.Domain;

namespace HikingApp.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
