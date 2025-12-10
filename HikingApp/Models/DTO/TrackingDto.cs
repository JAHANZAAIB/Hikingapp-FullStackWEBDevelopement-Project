using System;

namespace HikingApp.Models.DTO
{
    public class StartTrackingRequestDto
    {
        public Guid RouteId { get; set; }
        public string RouteType { get; set; } = "Walk"; // "Walk" OR "UserRoute"
        public string RouteName { get; set; } = string.Empty;
    }

    public class UpdateTrackingRequestDto
    {
        public Guid TrackingId { get; set; }
        public double DistanceCoveredKM { get; set; }

        public double CurrentLatitude { get; set; }
        public double CurrentLongitude { get; set; }
        public string? GpsTrackFragment { get; set; }
    }

    public class CompleteTrackingRequestDto
    {
        public Guid TrackingId { get; set; }
    }

    public class ActivityTrackingResponseDto
    {
        public Guid Id { get; set; }
        public string RouteName { get; set; } = string.Empty;

        public double DistanceCoveredKM { get; set; }
        public string Status { get; set; } = "Active"; // Active, Completed

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? RouteType { get; set; }
        public string? GpsTrack { get; set; }
    }
}
