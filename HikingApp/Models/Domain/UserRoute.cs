using System;
using System.Collections.Generic;

namespace HikingApp.Models.Domain
{
    public class UserRoute
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string RouteGeometry { get; set; } = default!;
        public double DistanceKM { get; set; }
        public string DifficultyName { get; set; } = "Unknown";
        public bool IsPublic { get; set; } = false;
        public DateTime CreatedAt { get; set; }

        public List<ActivityTracking> ActivityTrackings { get; set; } = new();
    }
}
