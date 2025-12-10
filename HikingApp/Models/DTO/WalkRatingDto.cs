using System;

namespace HikingApp.Models.DTO
{
    public class AddRatingRequestDto
    {
        public int Rating { get; set; } // 1–5
        public string? Comment { get; set; }
    }

    public class WalkRatingResponseDto
    {
        public Guid Id { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
