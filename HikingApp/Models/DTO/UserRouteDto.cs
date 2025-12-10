using System;

namespace HikingApp.Models.DTO
{
    public class CreateUserRouteRequestDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string RouteGeometry { get; set; }
        public bool IsPublic { get; set; } = false;
    }

    public class UserRouteResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string RouteGeometry { get; set; }
        public double DistanceKM { get; set; }

        public string DifficultyName { get; set; }
        public bool IsPublic { get; set; }

        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
    }
}
