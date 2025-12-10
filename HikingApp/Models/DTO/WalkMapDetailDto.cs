using System;
using System.Collections.Generic;

namespace HikingApp.Models.DTO
{
public class WalkMapDetailDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public double LengthInKM { get; set; }

    public Guid RegionId { get; set; }
    public string RegionName { get; set; } = default!;

    public Guid DifficultyId { get; set; }
    public string DifficultyName { get; set; } = default!;

    public string? RouteGeometry { get; set; }
    public int? ElevationGainMeters { get; set; }
    public int? EstimatedDurationMinutes { get; set; }
    public bool IsAccessible { get; set; }
    public List<string>? Features { get; set; }

    public List<string> ImageUrls { get; set; } = new();

    public double AverageRating { get; set; }
    public int RatingsCount { get; set; }
}
}
