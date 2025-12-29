using HikingApp.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System.Text.Json;

namespace HikingApp.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkRating> WalkRatings { get; set; }
        public DbSet<Image> Images { get; set; }

        public DbSet<UserRoute> UserRoutes { get; set; }
        public DbSet<ActivityTracking> ActivityTrackings { get; set; }
        public DbSet<WalkDetails> WalkDetails { get; set; }


    }
}
