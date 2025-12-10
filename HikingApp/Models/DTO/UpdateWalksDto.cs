using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HikingApp.Models.DTO
{
    public class UpdateWalksDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; }
        public string? WalkImageUrl { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Length in KM must be greater than 0")]
        public double LengthInKM { get; set; }

        [Required(ErrorMessage = "DifficultyId is required")]
        public Guid DifficultyId { get; set; }

        [Required(ErrorMessage = "RegionId is required")]
        public Guid RegionId { get; set; }

        public string? RouteGeometry { get; set; }
        public int? ElevationGainMeters { get; set; }
        public int? EstimatedDurationMinutes { get; set; }
        public bool IsAccessible { get; set; } = true;
        public List<string>? Features { get; set; }
    }
}
