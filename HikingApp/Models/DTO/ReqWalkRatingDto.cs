using System.ComponentModel.DataAnnotations;

namespace HikingApp.Models.DTO
{
    public class ReqWalkRatingDto
    {
        [Required]
        public Guid WalkId { get; set; }
        
        [Range(1, 5)]
        public int Rating { get; set; }
        
        public string? Comment { get; set; }
    }
}
