using System;
using System.Collections.Generic;

namespace HikingApp.Models.DTO
{
    public class NewWalkResponseDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public double LengthInKM { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }
        public string DifficultyName { get; set; }

        public Guid RegionId { get; set; }
        public string RegionName { get; set; }

        public string? RouteGeometry { get; set; }
        public int? ElevationGainMeters { get; set; }
        public int? EstimatedDurationMinutes { get; set; }

        public bool IsAccessible { get; set; }
        public List<string>? Features { get; set; }

        public double AverageRating { get; set; }
        public int TotalRatings { get; set; }
    }


    public class NewAddWalkRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public double LengthInKM { get; set; }
        public string? WalkImageUrl { get; set; }

        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }

        public string? RouteGeometry { get; set; }
        public int? ElevationGainMeters { get; set; }
        public int? EstimatedDurationMinutes { get; set; }

        public bool IsAccessible { get; set; } = true;
        public List<string>? Features { get; set; }
    }


    public class NewUpdateWalkRequestDto : NewAddWalkRequestDto { }
}
