using System.ComponentModel.DataAnnotations;

namespace HikingApp.Models.DTO
{
    public class AddRequestDto
    {
        [Required(ErrorMessage = "Code is required")]
        [StringLength(3, ErrorMessage = "Code cannot be longer than 3 characters")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot be longer than 50 characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
