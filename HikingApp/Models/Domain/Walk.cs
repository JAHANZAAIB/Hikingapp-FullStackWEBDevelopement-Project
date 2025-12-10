using System;
using System.Collections.Generic;

namespace HikingApp.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKM {  get; set; }
        public string? WalkImageUrl  { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }


        public Difficulty difficulty { get; set; }
        public Region Region { get; set; }

        public string? RouteGeometry { get; set; }
        public int? ElevationGainMeters { get; set; }
        public int? EstimatedDurationMinutes { get; set; }
        public bool IsAccessible { get; set; } = true;
        public List<string> Features { get; set; } = new();

        public List<WalkRating> Ratings { get; set; } = new();
        public List<Image> Images { get; set; } = new();
        public List<ActivityTracking> Activities { get; set; } = new();
    }
}
