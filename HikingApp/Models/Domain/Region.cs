using System;
using System.Collections.Generic;

namespace HikingApp.Models.Domain
{
    public class Region
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }

        public List<Walk> Walks { get; set; } = new();

    }
}
