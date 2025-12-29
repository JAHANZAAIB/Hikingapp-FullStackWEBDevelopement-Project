using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HikingApp.Models.Domain
{
    public class WalkDetails
    {
        [Key]
        public Guid Id { get; set; }
        
        public Guid WalkId { get; set; }
        
        public string? RouteGeometry { get; set; }
        public int? ElevationGainMeters { get; set; }
        public int? EstimatedDurationMinutes { get; set; }
        public bool IsAccessible { get; set; } = true;
        
        public List<string> Features { get; set; } = new();

        [JsonIgnore]
        [ForeignKey("WalkId")]
        public Walk Walk { get; set; }
    }
}
