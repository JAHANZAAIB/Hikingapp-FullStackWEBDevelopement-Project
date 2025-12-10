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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Walk → Difficulty (many-to-one)
            modelBuilder.Entity<Walk>()
                .HasOne(w => w.difficulty)
                .WithMany(d => d.Walks)
                .HasForeignKey(w => w.DifficultyId)
                .OnDelete(DeleteBehavior.Restrict);

            // Walk → Region (many-to-one)
            modelBuilder.Entity<Walk>()
                .HasOne(w => w.Region)
                .WithMany(r => r.Walks)
                .HasForeignKey(w => w.RegionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Walk → WalkRating (1-to-many)
            modelBuilder.Entity<WalkRating>()
                .HasOne(r => r.Walk)
                .WithMany(w => w.Ratings)
                .HasForeignKey(r => r.WalkId)
                .OnDelete(DeleteBehavior.Cascade);

            // Walk → Image (1-to-many)
            modelBuilder.Entity<Image>()
                .HasOne(i => i.Walk)
                .WithMany(w => w.Images)
                .HasForeignKey(i => i.WalkId)
                .OnDelete(DeleteBehavior.Cascade);

            // Walk → ActivityTracking
            modelBuilder.Entity<ActivityTracking>()
                .HasOne(a => a.Walk)
                .WithMany(w => w.Activities)
                .HasForeignKey(a => a.WalkId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ActivityTracking>()
                .HasOne(a => a.UserRoute)
                .WithMany(r => r.ActivityTrackings)
                .HasForeignKey(a => a.UserRouteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Features JSON Column stored as string
            var jsonOptions = new JsonSerializerOptions();
            var stringListConverter = new ValueConverter<List<string>, string>(
                v => JsonSerializer.Serialize(v ?? new List<string>(), jsonOptions),
                v => string.IsNullOrWhiteSpace(v)
                    ? new List<string>()
                    : JsonSerializer.Deserialize<List<string>>(v, jsonOptions) ?? new List<string>());

            modelBuilder.Entity<Walk>()
                .Property(w => w.Features)
                .HasConversion(stringListConverter)
                .HasColumnType("nvarchar(max)");
        }
    }
}
