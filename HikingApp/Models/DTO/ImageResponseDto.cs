using System.ComponentModel.DataAnnotations.Schema;

namespace HikingApp.Models.DTO
{
    public class ImageResponseDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; }
    }
}
