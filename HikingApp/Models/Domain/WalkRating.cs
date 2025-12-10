using System;

namespace HikingApp.Models.Domain
{
    public class WalkRating
    {
        public Guid Id { get; set; }
        public Guid WalkId { get; set; }
        public string UserId { get; set; } = default!;

        public int Rating { get; set; }   // 1–5
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Walk Walk { get; set; }
    }
}
