using System;
using System.Collections.Generic;

namespace HikingApp.Models.Domain
{
    public class Difficulty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Walk> Walks { get; set; } = new();
    }
}
