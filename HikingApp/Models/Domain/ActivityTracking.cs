using System;

namespace HikingApp.Models.Domain
{
    public class ActivityTracking
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = default!;

        public Guid? WalkId { get; set; }
        public Walk? Walk { get; set; }

        public Guid? UserRouteId { get; set; }
        public UserRoute? UserRoute { get; set; }

        public string RouteType { get; set; } = "Walk";
        public string RouteName { get; set; } = string.Empty;

        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime? EndTime { get; set; }

        public double DistanceCoveredKM { get; set; }
        public string Status { get; set; } = "Active";

        /// <summary>
        /// Stores the serialized GPS track for the activity. JSON array of [lat,lng] or object points.
        /// </summary>
        public string? GpsTrack { get; set; }
    }
}

