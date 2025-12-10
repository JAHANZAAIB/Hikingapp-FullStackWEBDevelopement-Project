using System.ComponentModel.DataAnnotations;

namespace HikingApp.Models.DTO
{
    public class ImageUploadReqDto
    {
        [Required]
        public string FileName { get; set; }
        [Required]
        public IFormFile File { get; set; }
        public String FileDescription { get; set; }
    }
}
